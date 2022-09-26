using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructures.Repositories
{
    public class ChapterSyllabusRepository : GenericRepository<ChapterSyllabus>,
                                             IChapterSyllabusRepository
    {
        public ChapterSyllabusRepository(AppDbContext dbContext,
                                         ICurrentTime currentTime,
                                         IClaimsService claimsService)
            : base(dbContext, currentTime, claimsService)
        { }
    }
}
