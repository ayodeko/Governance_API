using GovernancePortal.Core.General;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GovernancePortal.Service.Implementation
{
    public class UtilityService : IUtilityService
    {
        public readonly IHttpContextAccessor _httpContextAccessor;
        public UtilityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserModel GetUser()
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Jti).Value;
                var headers = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                //string companyId = headers[1];
                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                var email = _httpContextAccessor.HttpContext.User.FindFirst("email").Value;
                var firstName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
                var lastName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname).Value;

                var user = new UserModel()
                {
                    Id = userId,
                    CompanyId = "companyId",
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role
                };
                return user;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
