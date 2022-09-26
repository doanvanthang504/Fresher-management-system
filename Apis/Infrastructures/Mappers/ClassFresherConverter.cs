using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mappers
{
    public class ClassFresherConverter : ITypeConverter<ClassFresher, IEnumerable<FresherReport>>
    {
        public IEnumerable<FresherReport> Convert(ClassFresher source, IEnumerable<FresherReport> destination, ResolutionContext context)
        {
            foreach (var dto in source.Freshers.Select(e => context.Mapper.Map<FresherReport>(e)))
            {
                context.Mapper.Map(source, dto);
                dto.Id = Guid.NewGuid();
                dto.IsMonthly = true;
                yield return dto;
            }
        }
    }
}
