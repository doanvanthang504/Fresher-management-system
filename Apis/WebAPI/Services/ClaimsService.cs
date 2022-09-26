using Application.Interfaces;
using Domain.Enums;
using Global.Shared.Settings;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        public ClaimsService(
            IHttpContextAccessor httpContextAccessor)
        {
          
        }

        public Guid CurrentUserId { get; } = Guid.Empty;

        public RoleEnum CurrentUserRole { get; }

    }
}
