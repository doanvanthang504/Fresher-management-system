using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructures.Repositories
{
    public class LectureChapterRepository : GenericRepository<LectureChapter>,
                                            ILectureChapterRepository
    {
        public LectureChapterRepository(AppDbContext dbContext,
                                        ICurrentTime currentTime,
                                        IClaimsService claimsService)
             : base(dbContext, currentTime, claimsService)
        { }
    }
}
