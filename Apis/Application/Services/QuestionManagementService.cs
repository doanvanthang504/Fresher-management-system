using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Global.Shared.ViewModels.QuestionManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class QuestionManagementService : IQuestionManagementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionManagementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuditManagementResponse> AddRangeAsync(PostQuestionViewModel postQuestionViewModel)
        {
            if (postQuestionViewModel.questionManagementViewModels.Count == 0)
            {
                return null;
            }
            else
            {
                var listQuestionMap = _mapper.Map<List<QuestionManagement>>(postQuestionViewModel.questionManagementViewModels);
                foreach (var item in listQuestionMap)
                {
                    item.ModuleName = postQuestionViewModel.ModuleName;
                    item.NumberAudit = postQuestionViewModel.NumberAudit;
                }
                await _unitOfWork.QuestionManagementRepository.AddRangeAsync(listQuestionMap);
                if (await _unitOfWork.SaveChangeAsync() > 0)
                {
                    return new AuditManagementResponse
                    {
                        Data = postQuestionViewModel.questionManagementViewModels,
                        Message = "Insert success",
                        Success = true,
                    };
                }
                else
                {
                    return new AuditManagementResponse
                    {
                        Message = "Fail while save change",
                        Success = false,
                    };
                }
            }
        }

        public async Task<AuditManagementResponse> GetAllQuestionInPlanAuditAsync(GetQuestionToServer getQuestionToSerrver)
        {
            var listQuestion = await _unitOfWork.QuestionManagementRepository.GetAllQuestionInPlanAuditAsync(byte.Parse(getQuestionToSerrver.NumberAudit.ToString()),
                                                                                                        getQuestionToSerrver.ModuleName);
            if (listQuestion.Count == 0)
            {
                return new AuditManagementResponse
                {
                    Message = "Not data",
                    Success = true
                };
            }
            else
            {
                var result = _mapper.Map<List<GetQuestionViewModel>>(listQuestion);
                return new AuditManagementResponse
                {
                    Data = result,
                    Message = "Get success",
                    Success = true
                };
            }
        }
    }
}
