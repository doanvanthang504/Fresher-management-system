using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TopicViewModel> AddTopicAsync(CreateTopicViewModel createTopicViewModel)
        {
            //Check module is exist
            var module = await _unitOfWork.ModuleRepository.GetByIdAsync(createTopicViewModel.ModuleId);
            if (module == null)
            {
                throw new AppException(Constant.EXCEPTION_MODULE_NOT_FOUND, 404);
            }
            var topic = _mapper.Map<Topic>(createTopicViewModel);
            await _unitOfWork.TopicRepository.AddAsync(topic);
            var rowsAffect = await _unitOfWork.SaveChangeAsync() > 0;
            //Check SaveChange
            if (!rowsAffect)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var topicViewModel = _mapper.Map<TopicViewModel>(topic);
            return topicViewModel;
        }
        public async Task<Pagination<TopicViewModel>> GetAllTopicAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.TopicRepository.FindAsync(null, null, pageIndex, pageSize);
            var topicListModel = _mapper.Map<Pagination<TopicViewModel>>(result);
            return topicListModel;
        }

        public async Task<TopicViewModel> GetTopicByIdAsync(Guid topicId)
        {
            var result = await _unitOfWork.TopicRepository.GetByIdAsync(topicId);
            var topicModel = _mapper.Map<TopicViewModel>(result);
            return topicModel;
        }

        public async Task<List<TopicViewModel>> GetTopicByModuleIdAsync(Guid moduleId)
        {
            var topicList = await _unitOfWork.TopicRepository.GetByModuleId(moduleId);
            var topicModel = _mapper.Map<List<TopicViewModel>>(topicList);
            return topicModel;
        }
        public async Task<TopicViewModel> UpdateTopicAsync(Guid topicId, UpdateTopicViewModel updateTopicView)
        {
            var topic = await _unitOfWork.TopicRepository.GetByIdAsync(topicId);
            if (topic == null) 
            {
                throw new AppException(Constant.EXCEPTION_TOPIC_NOT_FOUND, 404);
            }
            topic = _mapper.Map(updateTopicView, topic);
            _unitOfWork.TopicRepository.Update(topic);
            var rowsAffectModule = await _unitOfWork.SaveChangeAsync() > 0;
            //Check SaveChange
            if (!rowsAffectModule) 
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var topicViewModel = _mapper.Map<TopicViewModel>(topic);
            return topicViewModel;
        }
    }
}

