using System;

namespace Global.Shared.ViewModels.PlanInfomationViewModels
{
    public class ChoosePlanForClassViewModel
    {
        public Guid ClassId { get; set; }

        public Guid PlanId { get; set; }

        //format:dd/MM/yy
        public string StartDate { get; set; }
    }
}
