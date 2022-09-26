using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using Global.Shared.ViewModels.ImportViewModels;
using System;

namespace Infrastructures.Mappers
{
    public class FresherConfigurationsProfile : Profile
    {
        public FresherConfigurationsProfile()
        {
            CreateMap<Fresher, FresherViewModel>()
                .ReverseMap();

            CreateMap<Fresher, Fresher>().
                ForMember(m => m.Id, o => o.Ignore()).
                ForMember(m => m.ClassFresherId, o => o.Ignore());

            CreateMap<FresherViewModel, FresherImportViewModel>().
                ForMember(
                    destinationFullName => destinationFullName.FullName,
                    options => options.MapFrom(source => source.FirstName + " " + source.LastName)
                    ).
                ForMember
                    (des => des.OnboardDate,
                    opts => opts
                        .MapFrom(source => source.OnBoard)).
                ReverseMap();

            CreateMap<FresherImportViewModel, FresherViewModel>().
                ForMember(
                    destinationFullName => destinationFullName.FirstName,
                    options => options.
                        MapFrom(source => source.FullName.
                        Substring(source.FullName.LastIndexOf(" "),
                        source.FullName.Length - source.FullName.LastIndexOf(" ")))
                    ).
                ForMember(
                    destinationFullName => destinationFullName.LastName,
                    options => options.
                        MapFrom(source => source.FullName.
                        Substring(0, source.FullName.LastIndexOf(" ")))
                    ).
                ForMember
                    (des => des.OnBoard,
                    opts => opts
                        .MapFrom(source => DateOnly.FromDateTime(source.OnboardDate)))
                .ForMember(
                    dest => dest.Status,
                    opts => opts.
                        MapFrom(source => (source.Status == "Onboard") ? StatusFresherEnum.Active : StatusFresherEnum.DropOut)).
                ForMember(
                    dest => dest.English,
                    opts => opts.
                        MapFrom(source => source.Eng)).
                ForMember(
                    dest => dest.ContactType,
                    opts => opts.MapFrom(source => source.ContractType)).
                    ForMember(
                    dest => dest.DOB,
                    opts => opts.MapFrom(source => DateOnly.FromDateTime(source.Dob.Value))).
                   
                ReverseMap();

            CreateMap<Fresher, ChangeStatusFresherViewModel>().ReverseMap();

            CreateMap<Fresher, FresherAttendancesViewModel>();

            CreateMap<Fresher, Attendance>()
                .ForMember(dest => dest.FresherId, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, src => src.Ignore());
        }
    }
}
