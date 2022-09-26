using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Global.Shared.ViewModels.PlanViewModels;
using Global.Shared.ViewModels.SyllabusDetailViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<PlanInformationViewModel>> ChoosePlanForClassAsync(
                                                ChoosePlanForClassViewModel choosePlanForClassViewModel)
        {
            var classId = choosePlanForClassViewModel.ClassId;
            var planId = choosePlanForClassViewModel.PlanId;
            DateTime startDate = DateTime.ParseExact(choosePlanForClassViewModel.StartDate,
                                                    "dd/MM/yyyy", 
                                                    CultureInfo.InvariantCulture);
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            if (plan == null) throw new Exception(Constant.EXCEPTION_PLAN_NOT_FOUND);
            //Find Module by PlanId
            var modules = await _unitOfWork.ModuleRepository.GetModuleByPlanId(planId);
            if (modules == null) throw new Exception(Constant.EXCEPTION_LIST_MODULE_NOT_FOUND);
            //new list PlanInformation
            var planInfoAddViewModels = new List<PlanInformationViewModel>();
            for (int i = 0; i < modules.Count; i++)
            {
                var topics = await _unitOfWork.TopicRepository.GetByModuleId(modules[i].Id);
                var itemPlanInfoAddViewModels = _mapper.Map<List<PlanInformationViewModel>>(topics);
                planInfoAddViewModels.AddRange(itemPlanInfoAddViewModels);
            }
            planInfoAddViewModels = planInfoAddViewModels.Select(x => { x.ClassId = classId; return x; }).ToList();
            var planInfoAfterSetDate = SetDateForPlanInfomation(planInfoAddViewModels, startDate);
            var planInformations = _mapper.Map<List<PlanInformation>>(planInfoAfterSetDate);
            //AddRange PlanInformation
            await _unitOfWork.PlanInformationRepository.AddRangeAsync(planInformations);
            await _unitOfWork.SaveChangeAsync();
            var planViewModelResult = _mapper.Map<List<PlanInformationViewModel>>(planInformations);
            //Generate SyllabusDetail for class
            var syllabusDetailAddViewModel =await GenerateSyllabusDetail(classId, planId);
            //set PlanInformationId for SyllabusDetail
            var syllabusDetailThenSetPlanInformationId = SetPlanInfomationIdForSyllabusDetail(
                                                            planViewModelResult, syllabusDetailAddViewModel);
            var syllabusDetail = _mapper.Map<List<SyllabusDetail>>(syllabusDetailThenSetPlanInformationId);
            await _unitOfWork.SyllabusDetailRepository.AddRangeAsync(syllabusDetail);
            var rowsAffect = await _unitOfWork.SaveChangeAsync()>0;
            if (!rowsAffect) throw new Exception(Constant.EXCEPTION_SAVECHANGE_FAILED);
            return planViewModelResult;
        }
        private static List<SyllabusDetailAddViewModel> SetPlanInfomationIdForSyllabusDetail(
                                                    List<PlanInformationViewModel> planInformationViewModels,
                                                    List<SyllabusDetailAddViewModel> syllabusDetailAddViewModels)
        {
            for(int i=0; i<planInformationViewModels.Count;i++)
            {
                for(int j=0; j<syllabusDetailAddViewModels.Count;j++)
                {
                    if (planInformationViewModels[i].TopicName == syllabusDetailAddViewModels[j].TopicName)
                    {
                        syllabusDetailAddViewModels[j].PlanInformationId = planInformationViewModels[i].Id;
                    }
                }
            }
            return syllabusDetailAddViewModels;
        }
        private async Task<List<SyllabusDetailAddViewModel>> GenerateSyllabusDetail(Guid classId, Guid planId)
        {
            var module = await _unitOfWork.ModuleRepository.GetModuleByPlanId(planId);

            var topics = new List<Topic>();
            foreach(var item in module)
            {
                //Get list topic by module id
                topics.AddRange(await _unitOfWork.TopicRepository.GetByModuleId(item.Id));
            }
            var chapters = new List<ChapterSyllabus>();
            foreach (var item in topics)
            {
                chapters.AddRange(await _unitOfWork.ChapterSyllabusRepository.FindAsync(x => x.TopicId == item.Id, null));
            }
            var lectures = new List<LectureChapter>();
            foreach (var item in chapters)
            {
                lectures.AddRange(await _unitOfWork.LectureChapterRepository.FindAsync(x => x.ChapterSyllabusId == item.Id, null));
            }
            var syllabusDetailViewModel = _mapper.Map<List<SyllabusDetailAddViewModel>>(lectures);
            syllabusDetailViewModel = syllabusDetailViewModel.Select(x => { x.ClassId = classId; return x; }).ToList();
            return syllabusDetailViewModel;
        }
        private static List<PlanInformationViewModel> SetDateForPlanInfomation(
                                            List<PlanInformationViewModel> planInfoViewModels, DateTime startDate)
        {
            for (int j = 0; j < planInfoViewModels.Count; j++)
            {
                //Skip 2 day if StartDate == Saturday and 1 day StartDate == Sunday
                if (startDate.DayOfWeek == DayOfWeek.Saturday) startDate = startDate.AddDays(2);
                if (startDate.DayOfWeek == DayOfWeek.Sunday) startDate = startDate.AddDays(1);
                //Set StartDate For list
                planInfoViewModels[j].StartDate = DateOnly.FromDateTime(startDate);
                // conver duration from minute to day number
                var durationRounding = Math.Round(Convert.ToDouble(planInfoViewModels[j].Duration / 8 / 60));
                var endDate = startDate.AddDays(durationRounding - 1);
                //Skip day if is weekend
                for (var date = planInfoViewModels[j].StartDate; date <= DateOnly.FromDateTime(endDate); date = date.Value.AddDays(1))
                {
                    if ((date.Value.DayOfWeek == DayOfWeek.Saturday) || (date.Value.DayOfWeek == DayOfWeek.Sunday))
                    {
                        endDate = endDate.AddDays(1);
                    }
                }
                //Set EndDate For List
                planInfoViewModels[j].EndDate = DateOnly.FromDateTime(endDate);
                //Set StartDate For Next Topic = EndDate Old Topic + 1 day
                startDate = endDate.AddDays(1);
            };
            return planInfoViewModels;
        }
        public async Task<PlanGetViewModel> AddPlanAsync(PlanAddViewModel planAddViewModel)
        {
            var plan = _mapper.Map<Plan>(planAddViewModel);
            await _unitOfWork.PlanRepository.AddAsync(plan);
            var rowsAffect = await _unitOfWork.SaveChangeAsync() == 1;
            if (!rowsAffect) throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            var planViewModel = _mapper.Map<PlanGetViewModel>(plan);
            return planViewModel;
        }
        public async Task<Pagination<PlanGetViewModel>> GetAllPlanAsync(int pageIndex, int pageSize)
        {
            var plans = await _unitOfWork.PlanRepository.FindAsync(null, null, pageIndex, pageSize);
            var planViewModels = _mapper.Map<Pagination<PlanGetViewModel>>(plans);
            return planViewModels;
        }
        public async Task<PlanGetViewModel> GetPlanByIdAsync(Guid planId)
        {
            var plan = await _unitOfWork.PlanRepository.FindAsync(planId, x => x.Modules);
            if (plan == null) throw new AppException(Constant.EXCEPTION_PLAN_NOT_FOUND, 404);
            var planViewModel = _mapper.Map<PlanGetViewModel>(plan);
            return planViewModel;
        }
        public async Task<PlanGetViewModel> UpdatePlanAsync(Guid planId,PlanUpdateViewModel planUpdate)
        {
            //Check item is exist
            var plan = await _unitOfWork.PlanRepository.GetByIdAsync(planId);
            if (plan == null) throw new AppException(Constant.EXCEPTION_PLAN_NOT_FOUND, 404);
            plan = _mapper.Map(planUpdate, plan);
            //update item planEntity
            _unitOfWork.PlanRepository.Update(plan);
            var rowsAffected = await _unitOfWork.SaveChangeAsync() == 1;
            if (!rowsAffected) throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            var planViewModels = _mapper.Map<PlanGetViewModel>(plan);
            return planViewModels;
        }
        //public async Task SeedDataPlans()
        //{
        //    //AddRange Plan
        //    var dataPlan = DataInnitializer.SeedData<Plan>(Constant.DATA_PLAN);
        //    await _unitOfWork.PlanRepository.AddRangeAsync(dataPlan);
        //    //AddRange Module
        //    var dataModule = DataInnitializer.SeedData<Module>(Constant.DATA_MMODULE);
        //    await _unitOfWork.ModuleRepository.AddRangeAsync(dataModule);
        //    //AddRangeTopic
        //    var dataTopic = DataInnitializer.SeedData<Topic>(Constant.DATA_TOPIC);
        //    await _unitOfWork.TopicRepository.AddRangeAsync(dataTopic);
        //    await _unitOfWork.SaveChangeAsync();
        //}
    }
}
