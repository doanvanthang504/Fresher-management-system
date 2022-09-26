using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFeedbackAnswerRepository: IGenericRepository<FeedbackAnswer>
    {
        /// <summary>
        /// constrainId is Id of constrain of Model, it is should be feedbackId or questionId
        /// model is parent model 
        /// </summary>
        /// <param name="constrainId"></param>
        /// <returns></returns>
        public Task<List<FeedbackAnswer>> GetFeedbackAnswerByConstrainIdAsync(Type model, Guid constrainId);
    }
}
