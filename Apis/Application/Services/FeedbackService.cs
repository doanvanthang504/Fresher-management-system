using Application.Interfaces;
using Application.SeedData;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.FeedbackViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FeedbackViewModel?> CreateFeedbackAsync(CreateFeedbackViewModel feedback)
        {
            var feedbackObj = _mapper.Map<Feedback>(feedback);
            await _unitOfWork.FeedbackRepository.AddAsync(feedbackObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackViewModel>(feedbackObj);
            }
            return null;
        }

        public async Task<FeedbackQuestionViewModel?> CreateFeedbackQuestionAsync(
            CreateFeedbackQuestionViewModel feedbackQuestion)
        {
            var feedbackQuestionObj = _mapper.Map<FeedbackQuestion>(feedbackQuestion);
            await _unitOfWork.FeedbackQuestionRepository.AddAsync(feedbackQuestionObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackQuestionViewModel>(feedbackQuestionObj);
            }
            return null;
        }

        public async Task<FeedbackResultViewModel?> CreateFeedbackResultAsync(
                                                        CreateFeedbackResultViewModel feedbackResult)
        {
            var feedbackResultObj = _mapper.Map<FeedbackResult>(feedbackResult);
            await _unitOfWork.FeedbackResultRepository.AddAsync(feedbackResultObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackResultViewModel>(feedbackResultObj);
            }
            return null;
        }

        public async Task DeleteFeedbackAsync(Guid feedbackId)
        {
            var feedbackObj = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedbackObj == null)
            {
                throw new AppNotFoundException($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {feedbackId}");
            }
          
            var searchfeedbackQuestionViewModel = new SearchFeedbackQuestionViewModel() 
            {
                FeedbackId=feedbackId 
            };
            var questionsOfFeedback =(await _unitOfWork.FeedbackQuestionRepository
                                                            .SearchAsync(searchfeedbackQuestionViewModel))
                                                            .Items;
            var anserOfFeedback = (await _unitOfWork.FeedbackAnswerRepository
                                                        .GetFeedbackAnswerByConstrainIdAsync(typeof(Feedback),feedbackId));

            _unitOfWork.FeedbackAnswerRepository.SoftRemoveRange(anserOfFeedback);
            _unitOfWork.FeedbackQuestionRepository.SoftRemoveRange(questionsOfFeedback);
            _unitOfWork.FeedbackRepository.SoftRemove(feedbackObj);
           
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSuccess)
            {
                throw new AppArgumentInvalidException(Constant.EXCEPTION_REMOVE_FAILED);
            }
        }

        public async Task DeleteFeedbackQuestionAsync(Guid id)
        {
            var feedbackQuestionObj = await _unitOfWork
                                                .FeedbackQuestionRepository
                                                .GetByIdAsync(id);
            if (feedbackQuestionObj == null)
            {
                throw new Exception($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_QUESTION} {id}");
            }
            _unitOfWork.FeedbackQuestionRepository.SoftRemove(feedbackQuestionObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSuccess)
            {
                throw new Exception(Constant.EXCEPTION_REMOVE_FAILED);
            }
        }

        public async Task<List<FeedbackResultViewModel>> GetAllResultOfFeedbackByFeedbackIdAsync(Guid feedbackId)
        {
            var feedbackObj = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedbackObj == null)
            {
                throw new Exception($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {feedbackId}");
            }
            var feedbackResults = await _unitOfWork
                                                .FeedbackResultRepository
                                                .GetAllResultOfFeedbackAsync(feedbackObj.Id);
            var feedbackResultsResponse = _mapper.Map<List<FeedbackResultViewModel>>(feedbackResults);
            return feedbackResultsResponse;
        }

        public async Task<FeedbackViewModel?> GetFeedbackByIdAsync(Guid id)
        {
            var feedbackObj = await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
            var feedbackObjResponse = _mapper.Map<FeedbackViewModel>(feedbackObj);
            return feedbackObjResponse;
        }

        public async Task<FeedbackQuestionViewModel?> GetFeedbackQuestionByIdAsync(Guid questionId)
        {
            var feedbackQuestionObj = await _unitOfWork.FeedbackQuestionRepository.GetByIdAsync(questionId);
            var feedbackQuestionObjResponse = _mapper.Map<FeedbackQuestionViewModel>(feedbackQuestionObj);
            return feedbackQuestionObjResponse;
        }

        public async Task<FeedbackAnswerViewModel?> GetFeedbackAnswerByIdAsync(Guid feedbackAnswerId)
        {
            var feedbackAnswerObj = await _unitOfWork.FeedbackAnswerRepository.GetByIdAsync(feedbackAnswerId);
            var feedbackAnswerObjResponse = _mapper.Map<FeedbackAnswerViewModel>(feedbackAnswerObj);
            return feedbackAnswerObjResponse;
        }

        public async Task<FeedbackResultViewModel?> GetFeedbackResultByIdAsync(Guid feedbackResultId)
        {
            var feedbackResultObj = await _unitOfWork.FeedbackResultRepository.GetByIdAsync(feedbackResultId);
            var feedbackResultObjResponse = _mapper.Map<FeedbackResultViewModel>(feedbackResultObj);
            return feedbackResultObjResponse;
        }

        public async Task<Pagination<FeedbackViewModel>> SearchFeedbackAsync(SearchFeedbackViewModel searchFeedback)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.SearchAsync(searchFeedback);
            var results = _mapper.Map<Pagination<FeedbackViewModel>>(feedbacks);
            return results;
        }

        public async Task<Pagination<FeedbackQuestionViewModel>> SearchFeedbackQuestionAsync(
            SearchFeedbackQuestionViewModel searchFeedbackQuestion)
        {
            var feedbacks = await _unitOfWork
                                        .FeedbackQuestionRepository
                                        .SearchAsync(searchFeedbackQuestion);
            var results = _mapper.Map<Pagination<FeedbackQuestionViewModel>>(feedbacks);
            return results;
        }

        public async Task<Pagination<FeedbackResultViewModel>> SearchFeedbackResultAsync(SearchFeedbackResultViewModel searchFeedbackResult)
        {
            var feedbacks = await _unitOfWork.FeedbackResultRepository
                                                    .SearchAsync(searchFeedbackResult);
            var results = _mapper.Map<Pagination<FeedbackResultViewModel>>(feedbacks);
            return results;
        }

        public async Task<FeedbackViewModel> UpdateFeedbackAsync(UpdateFeedbackViewModel feedback)
        {
            var feedbackObj = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedback.FeedbackId);
            if (feedbackObj == null)
            {
                throw new Exception($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {feedback.FeedbackId}");
            }
            _mapper.Map(feedback, feedbackObj);
            _unitOfWork.FeedbackRepository.Update(feedbackObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackViewModel>(feedbackObj);
            }
            return new FeedbackViewModel();
        }

        public async Task<FeedbackQuestionViewModel> UpdateFeedbackQuestionAsync(UpdateFeedbackQuestionViewModel feedbackQuestion)
        {
            var feedbackQuestionObj = await _unitOfWork.FeedbackQuestionRepository.GetByIdAsync(feedbackQuestion.FeedbackId);
            if (feedbackQuestionObj == null)
            {
                throw new Exception($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_QUESTION} {feedbackQuestion.FeedbackId}");
            }
            _mapper.Map(feedbackQuestion, feedbackQuestionObj);
            _unitOfWork.FeedbackQuestionRepository.Update(feedbackQuestionObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackQuestionViewModel>(feedbackQuestionObj);
            }
            return null;
        }

        public async Task<FeedbackAnswerViewModel> UpdateFeedbackAnswerAsync(UpdateFeedbackAnswerViewModel feedbackAnswer)
        {
            var feedbackAnswerObj = await _unitOfWork.FeedbackAnswerRepository.GetByIdAsync(feedbackAnswer.AnswerId);
            if (feedbackAnswerObj == null)
            {
                throw new AppNotFoundException($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_ANSWER} {feedbackAnswer.AnswerId}");
            }
            _mapper.Map(feedbackAnswer, feedbackAnswerObj);
            _unitOfWork.FeedbackAnswerRepository.Update(feedbackAnswerObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackAnswerViewModel>(feedbackAnswerObj);
            }
            return null;
        }

        public async Task<FeedbackAnswerViewModel?> CreateFeedbackAnswerAsync(CreateFeedbackAnswerViewModel feedbackAnswer)
        {
            var feedbackAnswerObj = _mapper.Map<FeedbackAnswer>(feedbackAnswer);
            await _unitOfWork.FeedbackAnswerRepository.AddAsync(feedbackAnswerObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (isSuccess)
            {
                return _mapper.Map<FeedbackAnswerViewModel>(feedbackAnswerObj);
            }
            return null;
        }
       
        public async Task<List<FeedbackAnswerViewModel>> GetFeedbackAnswerByConstrainIdAsync(Type model,Guid feedbackId)
        {
            var answers = await _unitOfWork.FeedbackAnswerRepository.GetFeedbackAnswerByConstrainIdAsync(model, feedbackId);
            var result = _mapper.Map<List<FeedbackAnswerViewModel>>(answers);
            return result;
        }

        public async Task DeleteFeedbackAnswerAsync(Guid id)
        {
            var feedbackAnswerObj = await _unitOfWork.FeedbackAnswerRepository.GetByIdAsync(id);
            if (feedbackAnswerObj == null)
            {
                throw new Exception($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_ANSWER} {id}");
            }
            _unitOfWork.FeedbackAnswerRepository.SoftRemove(feedbackAnswerObj);
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSuccess)
            {
                throw new Exception(Constant.EXCEPTION_REMOVE_FAILED);
            }
        }

    }
}
