using System;

namespace Global.Shared.ViewModels.ChapterSyllabusViewModels
{
    public class ChapterSyllabusAddViewModel
    {
        public string Name { get; set; }

        public Guid TopicId { get; set; }

        //Day start chapter in Core(Topic)
        //Duration of Core(Topic) = count all day learn chapter
        public int DayStart { get; set; }

        public int Order { get; set; }
    }
}
