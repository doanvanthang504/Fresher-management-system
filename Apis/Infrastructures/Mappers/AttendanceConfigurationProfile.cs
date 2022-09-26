using AutoMapper;
using Domain.Entities;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;

namespace Infrastructures.Mappers
{
    public class AttendanceConfigurationProfile : Profile
    {
        public AttendanceConfigurationProfile()
        {
            CreateMap<Attendance, AttendanceViewModel>().ReverseMap();
            CreateMap<CreateAttendanceViewModel, Attendance>();
            CreateMap<Attendance, UpdateAttendanceViewModel>().ReverseMap();
            CreateMap<ReportAttendance, ReportAttendanceViewModel>().ReverseMap();
            CreateMap<GenerateAttendanceTokenViewModel, GenerateAttendanceClassTokenViewModel>().ReverseMap();
        }
    }
}
