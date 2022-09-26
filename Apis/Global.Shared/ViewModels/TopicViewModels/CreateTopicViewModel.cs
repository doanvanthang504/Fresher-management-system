using System;

namespace Global.Shared.ViewModels.TopicViewModels
{
    public class CreateTopicViewModel
    {
        public string? Name { get; set; }

        public Guid ModuleId { get; set; }

        public int Order { get; set; }

        public string Pic { get; set; }

        public double Duration { get; set; }

        public string? NoteDetail { get; set; }
    }
}
