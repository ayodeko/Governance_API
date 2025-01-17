﻿using GovernancePortal.Core.General;
using GovernancePortal.Service.ClientModels.Exceptions;
using GovernancePortal.Service.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var companyId = _httpContextAccessor.HttpContext.Request.Headers["companyId"];
                var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value ?? "";
                var imageId = _httpContextAccessor.HttpContext.User.FindFirst("profilePic")?.Value ?? "";
                var email = _httpContextAccessor.HttpContext.User.FindFirst("email").Value ?? "";
                var firstName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value ?? "";
                var lastName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname).Value ?? "";

                var user = new UserModel()
                {
                    Id = userId,
                    CompanyId = companyId,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Role = role
                };
                Global.User = user;
                return user;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }
    }
}
