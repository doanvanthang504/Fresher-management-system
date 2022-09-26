using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class StreamExtension
    {
        public static Task<string> ReadAllTextAsync(this Stream fileStream)
        {
            using var streamReader = new StreamReader(fileStream);
            return streamReader.ReadToEndAsync();
           
        }
        public static double GetScore(this IList<Score>? scores,
                                            Guid fresherId,
                                            TypeScoreEnum typeScoreEnum)
        {
            return scores.Where(x => x.FresherId == fresherId
                                && x.TypeScore == typeScoreEnum)
                         .Select(x => x.ModuleScore)
                         .FirstOrDefault();
        }
    }
}