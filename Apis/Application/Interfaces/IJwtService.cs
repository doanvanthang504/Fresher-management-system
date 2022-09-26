using Domain.Entities;
using Global.Shared.ViewModels.TokenObjectViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        public TokenObject GenerateToken(User user);
    }
}
