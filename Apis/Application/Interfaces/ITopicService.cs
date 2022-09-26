using Global.Shared.Commons;
using Global.Shared.ViewModels.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITopicService
    {
        Task<Pagination<TopicViewModel>> GetAllTopicAsync(int pageIndex, int pageSize);
        Task<TopicViewModel> GetTopicByIdAsync(Guid topicId);

        Task<List<TopicViewModel>> GetTopicByModuleIdAsync(Guid moduleId);
        Task<TopicViewModel> AddTopicAsync(CreateTopicViewModel createTopicViewModel);
        Task<TopicViewModel> UpdateTopicAsync(Guid topicId, UpdateTopicViewModel topic);
    }
}
