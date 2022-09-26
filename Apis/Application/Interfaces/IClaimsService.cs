using Domain.Enums;
using System;

namespace Application.Interfaces
{
    public interface IClaimsService
    {
        public Guid CurrentUserId { get; }

        public RoleEnum CurrentUserRole { get; }

    }
}
