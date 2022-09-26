using Domain.Enums;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Username { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? HashedPassword { get; set; } = null!;

        public RoleEnum? Role { get; set; }

        public string? Phone { get; set; }

        public bool IsBanned { get; set; }

        // Auditor
        public string? Skills { get; set; }
    }
}
