using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.FresherViewModels
{
    public class ChangeStatusFresherViewModel
    {
        public Guid Id { get; set; }
        public StatusFresherEnum Status { get; set; }
    }
}
