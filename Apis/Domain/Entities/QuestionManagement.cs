namespace Domain.Entities
{
    public class QuestionManagement : BaseEntity
    {
        public string QuestionName { get; set; }

        public string ModuleName { get; set; }

        public byte NumberAudit { get; set; }
    }
}
