using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Global.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class FeedbackAnswerRepository : GenericRepository<FeedbackAnswer>, IFeedbackAnswerRepository
    {
        public FeedbackAnswerRepository(
           AppDbContext context,
           ICurrentTime timeService,
           IClaimsService claimsService)
           : base(context,
                 timeService,
                 claimsService)
        {
        }

        public  async Task<List<FeedbackAnswer>> GetFeedbackAnswerByConstrainIdAsync(Type entity, Guid constrainId)
        {
            if (entity == typeof(Feedback))
            {
                return await _dbSet.Where(x=>x.Question.FeedbackId== constrainId).ToListAsync();
            }
            else if (entity == typeof(FeedbackQuestion))
            {
                return  await _dbSet.Where(x=>x.QuestionId==constrainId).ToListAsync();
            }
            else
            {
                throw new AppArgumentInvalidException("Argurment Invalid !");
            }
        }
    }
}
