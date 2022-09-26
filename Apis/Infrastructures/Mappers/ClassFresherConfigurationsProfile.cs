using AutoMapper;
using Domain.Entities;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Global.Shared.ViewModels.ImportViewModels;
using System;

namespace Infrastructures.Mappers
{
    public class ClassFresherConfigurationsProfile : Profile
    {
        public ClassFresherConfigurationsProfile()
        {
            CreateMap<ClassFresher, ClassFresherViewModel>().ReverseMap()
                .ForMember(dest => dest.Freshers,
                            opt => opt.MapFrom(x => x.Freshers));
            CreateMap<UpdateClassFresherInfoViewModel, ClassFresher>()               
                .ReverseMap();
            CreateMap<ClassFresher, ClassImportViewModel>().ReverseMap();
            CreateMap<ClassImportViewModel, ClassFresherViewModel>().ReverseMap();
            CreateMap<ClassFresher, UpdateClassFresherViewModel>().ReverseMap();
        }
    }
}
