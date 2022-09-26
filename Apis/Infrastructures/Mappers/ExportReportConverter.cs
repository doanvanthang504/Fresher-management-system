using AutoMapper;
using Domain.Entities;
using Global.Shared.ModelExport.ModelExportConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Mappers
{
    public class ExportReportConverter : ITypeConverter<ClassFresher, IEnumerable<ExportCourseReportViewModel>>
    {
        public IEnumerable<ExportCourseReportViewModel> Convert
            (ClassFresher source, 
             IEnumerable<ExportCourseReportViewModel> destination, 
             ResolutionContext context)
        {
            foreach (var dto in source.Freshers
                                      .Select(e => context.Mapper
                                                          .Map<ExportCourseReportViewModel>(e)))
            {
                context.Mapper.Map(source, dto);
                dto.Id = Guid.NewGuid();
                yield return dto;
            }
        }
    }
}
