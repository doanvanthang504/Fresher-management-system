using System;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTimeOffset CreationDate { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTimeOffset? ModificationDate { get; set; }

        public Guid? ModificationBy { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }

        public Guid? DeleteBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
