using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Exceptions;
using Global.Shared.Settings.Reminder;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Global.Shared.ViewModels.ReminderViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuditManagementService : IAuditManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReminderService _reminderService;

        public AuditManagementService(IUnitOfWork unitOfWork, IMapper mapper, IReminderService reminderService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reminderService = reminderService;
        }
        // get plan audit by class id
        public async Task<AuditManagementResponse> GetAuditByClassIdAsync(Guid classId)
        {
            var listAudit = await _unitOfWork.AuditManagementRepository.GetAuditbyClassIdAsync(classId);
            var listResult = _mapper.Map<List<GetPlanAuditViewModel>>(listAudit);
            return new AuditManagementResponse
            {
                Data = listResult,
                Success = true,
                Message = "Select success"
            };
        }
        // get evaluate audit by AVG score
        public Rank GetEvaluateAudit(double avgScore)
        {
            if (avgScore > 90 && avgScore <= 100)
            {
                return Rank.APlus;
            }
            else if (avgScore <= 90 && avgScore > 84)
            {
                return Rank.A;
            }
            else if (avgScore <= 84 && avgScore > 60)
            {
                return Rank.B;
            }
            else if(avgScore <= 60 && avgScore > 55)
            {
                return Rank.C;
            }
            else
            {
                return Rank.D;
            }
        }
        // update result audit for student
        public async Task<AuditManagementResponse> UpdateAuditForStudentAsync(Guid id,UpdateAuditViewModel updateAuditViewModel )
        {
            var listEvaluate = new List<Evaluate>
            {
                    (Evaluate)updateAuditViewModel.EvaluateQ1!,
                    (Evaluate)updateAuditViewModel.EvaluateQ2!,
                    (Evaluate)updateAuditViewModel.EvaluateQ3!,
                    (Evaluate)updateAuditViewModel.EvaluateQ4!
            };
            var listScore = await _unitOfWork.AuditManagementRepository.GetScoreFromEvaluateAsync(listEvaluate);
            var AvgScore = listScore.Sum() / listScore.Count;
            var checkAuditResult = await _unitOfWork.AuditManagementRepository.GetByIdAsync(id);
            if (checkAuditResult == null)
            {
                return null;
            }
            checkAuditResult = _mapper.Map(updateAuditViewModel, checkAuditResult);
            var auditUpdate = _mapper.Map<AuditResult>(checkAuditResult);
            auditUpdate.AuditScore = AvgScore;
            auditUpdate.Rank = GetEvaluateAudit(AvgScore);
            _unitOfWork.AuditManagementRepository.Update(auditUpdate);
            int check = await _unitOfWork.SaveChangeAsync();
            var responUpdateAudit = new AuditManagementResponse();
            if (check > 0)
            {
                responUpdateAudit = new AuditManagementResponse
                {
                    Data = auditUpdate,
                    Success = true,
                    Message = "Update success"
                };
            }
            else
            {
                responUpdateAudit = new AuditManagementResponse
                {
                    Success = false,
                    Message = "Error while SaveChange"
                };
            }
            return responUpdateAudit;
        }
        // delete audit result
        public async Task<AuditManagementResponse> DeleteAuditResultAsync(Guid Id)
        {
            var auditResult = await _unitOfWork.AuditManagementRepository.GetByIdAsync(Id);
            auditResult.IsDeleted = true;

            var result = _mapper.Map<AuditResult>(auditResult);
            _unitOfWork.AuditManagementRepository.Update(result);
            var check = await _unitOfWork.SaveChangeAsync();
            if (check > 0)
                return new AuditManagementResponse
                {
                    Data = result,
                    Success = true,
                    Message = "Deleted Succesfull"
                };
            else
                return new AuditManagementResponse
                {
                    Success = false,
                    Message = "Error while SaveChange"
                };

        }
        // add auditor for plan audit
        public async Task<AuditManagementResponse> AddAuditorForPlanAuditAsync(GetAuditAndAuditorViewModel getAuditAndAuditor)
        {
            var listAuditResult = await _unitOfWork.AuditManagementRepository
                                                   .GetPlanAuditByClassIdAndNameModuelAsync(getAuditAndAuditor.GetAuditByClassIdAndNumberAuditViewModel);

            var surplus = listAuditResult.Count % getAuditAndAuditor.AuditorViewModels.Count;
            var listAuditResultMap = _mapper.Map<List<AuditResult>>(listAuditResult);
            int jumpNumber = listAuditResult.Count / getAuditAndAuditor.AuditorViewModels.Count;
            int jump = 0;
            if (surplus > 1)
            {
                for (int i = 0; i < listAuditResult.Count - surplus; i++)
                {
                    if (i != 0 && i % jumpNumber == 0)
                        jump++;
                    listAuditResultMap[i].AuditorId = getAuditAndAuditor.AuditorViewModels[jump].Id.ToString();

                }
                jump = 0;
                for (int i = listAuditResult.Count - surplus; i < listAuditResult.Count; i++)
                {
                    if (jump == jumpNumber)
                        jump = 0;
                    listAuditResultMap[i].AuditorId = getAuditAndAuditor.AuditorViewModels[jump].Id.ToString();
                    jump++;
                }
            }
            else
            {
                for (int i = 0; i < listAuditResult.Count; i++)
                {
                    if (i != 0 && i != jumpNumber * getAuditAndAuditor.AuditorViewModels.Count && i % jumpNumber == 0)
                        jump++;
                    listAuditResultMap[i].AuditorId = getAuditAndAuditor.AuditorViewModels[jump].Id.ToString();
                }
            }
            _unitOfWork.AuditManagementRepository.UpdateRange(listAuditResultMap);
            int check = await _unitOfWork.SaveChangeAsync();
            if (check > 0)
                return new AuditManagementResponse
                {
                    Data = listAuditResult,
                    Message = "add success",
                    Success = true
                };
            return new AuditManagementResponse
            {
                Message = "Error while save change",
                Success = false
            };
        }
        // Get plan audit
        public async Task<AuditManagementResponse> GetAllAuditPlansAsync()
        {
            var getListPlanAudit = await _unitOfWork.ClassFresherRepository.GetAllAsync();
            if (getListPlanAudit.Count == 0)
                return new AuditManagementResponse { Message = "Not Found", Success = true };
            else
            {
                var listPlanAuditViewModel = _mapper.Map<List<ClassViewModel>>(getListPlanAudit);
                return new AuditManagementResponse
                {
                    Data = listPlanAuditViewModel,
                    Message = "Get success",
                    Success = true
                };
            }
        }
        // get list auditor of class audit
        private async Task<List<Auditor>> ListAuditorOfClassAuditAsync(GetAuditByClassIdAndNumberAuditViewModel getAuditByClassIdAndNumberAudit)
        {
            var _dbSet = await _unitOfWork.AuditManagementRepository.GetAllAsync();

            var listIdAuditor =  _dbSet.Where(x => x.ClassFresherId.Equals(getAuditByClassIdAndNumberAudit.classId) &&
                                              x.ModuleName!.Equals(getAuditByClassIdAndNumberAudit.nameModule) &&
                                              x.NumberAudit.Equals(getAuditByClassIdAndNumberAudit.numberAudit))
                                       .GroupBy(x => Guid.Parse(x.AuditorId)).Select(x => x.Key).ToList();

            var listAuditor = new List<Auditor>();
            for (int i = 0; i < listIdAuditor.Count; i++)
            {
                var listAuditorSearch = await _unitOfWork.AuditorRepository.GetByIdAsync(listIdAuditor[i]);
                listAuditor.Add(listAuditorSearch!);
            }
            return listAuditor;
        }
        // get detail plan audit private
        private async Task<List<GetDetailPlanAuditViewModel>> GetDetailPlanAudit_Join_Async(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var listAuditor = await ListAuditorOfClassAuditAsync(auditViewModel);
            var _dbSet = await _unitOfWork.AuditManagementRepository.GetAllAsync();

            var listDetailPlan = _dbSet.Where(x => x.ClassFresherId.Equals(auditViewModel.classId) &&
                                                   x.ModuleName.Equals(auditViewModel.nameModule) &&
                                                   x.NumberAudit.Equals(auditViewModel.numberAudit))
                                       .ToList();
            var listDetailFresher = await _unitOfWork.FresherRepository.GetAllAsync();

            var listPlanJoinFresher = listDetailPlan.Join(
                                                                     listDetailFresher,
                                                                     plan => plan.FresherId,
                                                                     fresher => fresher.Id,
                                                                     (plan, fresher) => new GetDetailPlanAuditViewModel
                                                                     {
                                                                         Id = plan.Id,
                                                                         ClassFresherId = plan.ClassFresherId,
                                                                         ModuleName = plan.ModuleName,
                                                                         NumberAudit = plan.NumberAudit,
                                                                         FresherId = plan.FresherId,
                                                                         AuditorId = plan.AuditorId,
                                                                         AccountName = fresher.AccountName,
                                                                         PracticeScore = plan.PracticeScore,
                                                                         Rank = plan.Rank.ToString(),

                                                                         QuestionQ1 = plan.QuestionQ1,
                                                                         QuestionQ2 = plan.QuestionQ2,
                                                                         QuestionQ3 = plan.QuestionQ3,
                                                                         QuestionQ4 = plan.QuestionQ4,

                                                                         EvaluateQ1 = plan.EvaluateQ1,
                                                                         EvaluateQ2 = plan.EvaluateQ2,
                                                                         EvaluateQ3 = plan.EvaluateQ3,
                                                                         EvaluateQ4 = plan.EvaluateQ4,

                                                                         CommentQ1 = plan.CommentQ1,
                                                                         CommentQ2 = plan.CommentQ2,
                                                                         CommentQ3 = plan.CommentQ3,
                                                                         CommentQ4 = plan.CommentQ4,

                                                                         PracticeComment = plan.PracticeComment,
                                                                         AuditScore = plan.AuditScore,
                                                                         AuditComment = plan.AuditComment,
                                                                         Status = plan.Status
                                                                     }
                                                          ).ToList();

            var listPlanJoinFresherAndAuditor = listPlanJoinFresher.Join(
                                                                          listAuditor,
                                                                          pf => pf.AuditorId,
                                                                          auditor => auditor.Id.ToString(),
                                                                          (pf, auditor) => new GetDetailPlanAuditViewModel
                                                                          {
                                                                              Id = pf.Id,
                                                                              ClassFresherId = pf.ClassFresherId,
                                                                              ModuleName = pf.ModuleName,
                                                                              NumberAudit = pf.NumberAudit,
                                                                              FresherId = pf.FresherId,
                                                                              AuditorId = pf.AuditorId,
                                                                              AuditorName = auditor.Username!,
                                                                              AccountName = pf.AccountName,
                                                                              PracticeScore = pf.PracticeScore,
                                                                              Rank = pf.Rank.ToString(),

                                                                              QuestionQ1 = pf.QuestionQ1,
                                                                              QuestionQ2 = pf.QuestionQ2,
                                                                              QuestionQ3 = pf.QuestionQ3,
                                                                              QuestionQ4 = pf.QuestionQ4,

                                                                              EvaluateQ1 = pf.EvaluateQ1,
                                                                              EvaluateQ2 = pf.EvaluateQ2,
                                                                              EvaluateQ3 = pf.EvaluateQ3,
                                                                              EvaluateQ4 = pf.EvaluateQ4,

                                                                              CommentQ1 = pf.CommentQ1,
                                                                              CommentQ2 = pf.CommentQ2,
                                                                              CommentQ3 = pf.CommentQ3,
                                                                              CommentQ4 = pf.CommentQ4,

                                                                              PracticeComment = pf.PracticeComment,
                                                                              AuditScore = pf.AuditScore,
                                                                              AuditComment = pf.AuditComment,
                                                                              Status = pf.Status,
                                                                          }
                                                                        ).ToList();

            return listPlanJoinFresherAndAuditor.OrderBy(x => x.AuditorName).ToList();
        }
        // get detail plan audit public
        public async Task<AuditManagementResponse> GetDetailPlanAuditAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var result = await GetDetailPlanAudit_Join_Async(auditViewModel);
            return new AuditManagementResponse
            {
                Data = result,
                Message = "Get success",
                Success = true
            };
        }
        // get all auditor
        public async Task<AuditManagementResponse> GetAllAuditorAsync()
        {
            var result = await _unitOfWork.AuditorRepository.GetAllAsync();
            if (result.Count == 0)
                return new AuditManagementResponse
                {
                    Message = "Not hava data",
                    Success = false
                };
            else
            {
                return new AuditManagementResponse
                {
                    Data = result,
                    Message = "Get data success",
                    Success = true
                };
            }
        }
        // get auditor of class module audit
        public async Task<List<AuditorViewModel>> GetAuditorOfClassModuleAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var listid = await _unitOfWork.AuditManagementRepository.GetAuditorByClassModuleAsync(auditViewModel);
            var result = new List<Auditor>();
            foreach (var item in listid)
            {
                var auditor = await _unitOfWork.AuditorRepository.GetByIdAsync(item);
                result.Add(auditor!);
            }
            var resultMap = _mapper.Map<List<AuditorViewModel>>(result);
            return resultMap;
        }
        // get all fresher in class
        public async Task<AuditManagementResponse> GetAllFresherIdInClassAsync(Guid classId)
        {
            var result = await _unitOfWork.FresherRepository.FindAsync(x => x.ClassFresherId == classId, null, null);
            if (result.Count == 0)
                return new AuditManagementResponse { Message = "Not Data", Success = true };
            else
            {
                var resultMap = _mapper.Map<List<FresherViewModel>>(result);
                return new AuditManagementResponse
                {
                    Data = resultMap,
                    Success = true,
                    Message = "Get data success"
                };
            }
        }
        // get detail fresher for class add fresher for plan audit
        private async Task<List<AddAuditViewModel>> AddFresherForPlanAuditAsync(CreateAuditViewModel createAuditViewModel)
        {
            var listAuditNew = new List<AddAuditViewModel>();

            int numberPlan = await _unitOfWork.AuditManagementRepository
                                                .GetClassFreshersAsync(createAuditViewModel.ClassFresherId);
            for (int i = 0; i < createAuditViewModel.fresherViewModels.Count; i++)
            {
                listAuditNew.Add(new AddAuditViewModel
                {
                    ClassFresherId = createAuditViewModel.ClassFresherId.ToString(),
                    ModuleName = createAuditViewModel.ModuleName,
                    FresherId = createAuditViewModel.fresherViewModels[i].Id.ToString(),
                    NumberAudit = byte.Parse((numberPlan + 1).ToString()),
                    QuestionQ1 = "none",
                    CommentQ1 = "none",

                    QuestionQ2 = "none",
                    CommentQ2 = "none",

                    QuestionQ3 = "none",
                    CommentQ3 = "none",

                    QuestionQ4 = "none",
                    CommentQ4 = "none",
                    DateStart = DateTime.Parse(createAuditViewModel.DateStart),

                    PracticeComment = "none",
                    PracticeScore = 0,
                    AuditScore = 0,

                    AuditComment = "none",
                    Status = false
                });
            }
            return listAuditNew;
        }
        // create remider for plan audit
        private async Task CreateRemiderForPlanAuditAsync(string moduleName, string className, string dateStart)
        {
            try
            {
                var reminder = new CreateReminderViewModel
                {
                    Description = "On the day " + dateStart + " have audit meeting for class " + moduleName,
                    Subject = "Audit module " + moduleName + " class " + className + " on " + dateStart,
                    EventTime = DateTime.Parse(dateStart),
                    ReminderType = ReminderType.Audit,
                    ReminderEmail = "tranvantiep2506@gmail.com"
                };
                await _reminderService.CreateReminderAsync(reminder);
            }
            catch
            {

            }
        }
        public async Task<AuditManagementResponse> CreatePlanAuditForMemberInClassAsync(CreateAuditViewModel createPlanAudit)
        {
            var dateStart = createPlanAudit.DateStart;
            var listAuditNew = await AddFresherForPlanAuditAsync(createPlanAudit);
            var lisAuditMapped = _mapper.Map<List<AuditResult>>(listAuditNew);
            var classFresher = await _unitOfWork.ClassFresherRepository
                                                .GetByIdAsync(Guid.Parse(listAuditNew[0].ClassFresherId));
            foreach (var audit in lisAuditMapped)
            {
                await _unitOfWork.AuditManagementRepository.AddAsync(audit);
            }
            if (await _unitOfWork.SaveChangeAsync() == lisAuditMapped.Count)
            {
                await CreateRemiderForPlanAuditAsync(lisAuditMapped[0].ModuleName!, classFresher!.ClassName!, dateStart);
                return new AuditManagementResponse
                {
                    Data = listAuditNew,
                    Message = "Add success",
                    Success = true
                };
            }
            return new AuditManagementResponse
            {
                Message = "Error while Savechange",
                Success = false
            };
        }

        public async Task<AuditManagementResponse> CountAuditorOfClassAsync(GetAuditByClassIdAndNumberAuditViewModel getNumberAuditor)
        {
            var listIdAuditor = await ListAuditorOfClassAuditAsync(getNumberAuditor);
            if (listIdAuditor.Count != 0)
            {
                if (listIdAuditor[0].Username == null)
                {
                    return new AuditManagementResponse
                    {
                        Data = 0,
                        Message = "Not have auditor",
                        Success = true
                    };
                }
            }
            return new AuditManagementResponse
            {
                Data = listIdAuditor.Count,
                Message = "Get have " + listIdAuditor.Count + " auditor",
                Success = true
            };
        }
        // update information of plan audit
        public async Task<AuditManagementResponse> UpdateInformationPlanAuditAsync(UpdateInformationPlanAudit updateInformationPlanAudit)
        {
            var planAudit = await _unitOfWork.AuditManagementRepository.GetAllAsync();
            var planAuditByModule = planAudit.Where(x => x.ModuleName == updateInformationPlanAudit.nameModuleOld &&
                                                         x.NumberAudit == updateInformationPlanAudit.numberAudit).ToList();
            if (planAuditByModule == null)
                return new AuditManagementResponse
                {
                    Message = "Not have plan audit",
                    Success = false
                };
            foreach(var item in planAuditByModule)
            {
                item.DateStart = DateTime.Parse(updateInformationPlanAudit.dateStart);
                item.ModuleName = updateInformationPlanAudit.nameModuleNew;
            }
            _unitOfWork.AuditManagementRepository.UpdateRange(planAuditByModule);
            if (await _unitOfWork.SaveChangeAsync() == planAuditByModule.Count)
                return new AuditManagementResponse
                {
                    Data = planAuditByModule,
                    Message = "Update success",
                    Success = true
                };
            return new AuditManagementResponse
            {
                Message = "Error while save change",
                Success = false
            };
        }

        public async Task<AuditResultResponse> GetAuditResultOfFresherInModuleAsync(AuditResultOfFresherInModuleViewModel getAuditResultOfFresher)
        {
            var listAuditResult = await _unitOfWork.AuditManagementRepository.GetAllAsync();
            var result = listAuditResult.Where(x => x.ClassFresherId == getAuditResultOfFresher.ClassFresherId &&
                                                    x.ModuleName == getAuditResultOfFresher.ModuleName &&
                                                    x.FresherId == getAuditResultOfFresher.FresherId).FirstOrDefault();
            if(result == null)
            {
                return new AuditResultResponse
                {
                    Message = "Not have data",
                    IsSuccess = false
                };
            }
            return new AuditResultResponse
            {
                Data = result,
                Message = "Get success",
                IsSuccess = true
            };
        }
    }
}
