namespace Domain.Entities
{
    public class Auditor : BaseEntity
    {
        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Skills { get; set; }

        public string? Note { get; set; }
    }
}
