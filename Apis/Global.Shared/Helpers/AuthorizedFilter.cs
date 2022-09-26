using Global.Shared.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Global.Shared.Helpers
{
    public class AuthorizedFilter : IActionFilter
    {
        private string[] _roles { get; set; }
       
        public AuthorizedFilter(string roles)
        {
            _roles = roles.Split(',');
        }
       
        public AuthorizedFilter()
        {
            _roles = new string[] { };
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            StringValues inputToken = string.Empty;
            var header = context.HttpContext.Request.Headers.TryGetValue("Authorization", out inputToken);
            if (header == false)
            {
                throw new AppUnauthorizedException("Unauthorized !");
            }

            var token = inputToken.ToString()
                                  .Substring(inputToken.ToString().IndexOf(" "))
                                  .Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var decodedValue = tokenHandler.ReadJwtToken(token);
                var claims = decodedValue.Claims;

                //Check expired
                var now = DateTime.UtcNow;
                var tokenExpiration = decodedValue.ValidTo;
                var timespan = (tokenExpiration - now).Seconds > 0;

                if (!timespan)
                {
                    throw new AppUnauthorizedException("Token Expired !");
                }

                //Check Role
                var role = claims.First(x => x.Type == ClaimTypes.Role).Value;
                if (!_roles.Contains(role))
                {
                    throw new AppForbiddenException("No Permission !");
                }
            }
            catch (ArgumentException)
            {
               throw new AppUnauthorizedException("Invalid Token !");
            }
           
        }
    }

    public class AuthorizedFilterAttribute : TypeFilterAttribute
    {
        public AuthorizedFilterAttribute(string Roles) : base(typeof(AuthorizedFilter))
        {
            Arguments = new object[] { Roles };
        }
    }
}
