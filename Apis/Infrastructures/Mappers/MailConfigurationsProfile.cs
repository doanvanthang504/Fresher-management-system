using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Constants;
using Global.Shared.ViewModels.MailViewModels;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;

namespace Infrastructures.Mappers
{
    public class MailConfigurationsProfile : Profile
    {
        public MailConfigurationsProfile()
        {
            CreateMap<MailMessage, MailViewModel>()
                .ForMember(dest => dest.ToAddresses,
                                opt => opt.MapFrom(src => string.Join(',', src.To.Select(t => t.Address))))
                .ForMember(dest => dest.CCAddresses,
                                opt => opt.MapFrom(src => string.Join(',', src.CC.Select(t => t.Address))))
                .ForMember(dest => dest.Body,
                                opt => opt.MapFrom(src => src.Body));

            CreateMap<MailRequestViewModel, MailMessage>()
                .ForMember(
                    dest => dest.To,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.CC,
                    opt => opt.Ignore())
                .ForMember(
                    dest => dest.Body,
                    opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.To.Add(src.ToAddresses!);

                    if (!string.IsNullOrEmpty(src.CCAddresses))
                        dest.CC.Add(src.CCAddresses);

                    if (!string.IsNullOrEmpty(src.Body))
                        SetBodyView(dest, src.Body);
                });
        }

        private void SetBodyView(MailMessage mail, string body)
        {
            // Replaces the current mail body.
            mail.AlternateViews.Clear();

            var bodyView = AlternateView.CreateAlternateViewFromString(body,
                                                new ContentType(HttpConstants.ContentType));

            mail.AlternateViews.Add(bodyView);
        }
    }
}
