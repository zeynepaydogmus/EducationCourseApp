using System.Collections.Generic;
using System.Threading.Tasks;
using EducationCourseApp.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace EducationCourseApp.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var isExistUser = await _userManager.FindByEmailAsync(context.UserName);
            
            if (isExistUser is null)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors",new List<string>{"Invalid email or password!"});
                context.Result.CustomResponse = errors;
                
                return;
            }

            var checkPassword = await _userManager.CheckPasswordAsync(isExistUser, context.Password);
            if (checkPassword == false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors",new List<string>{"Invalid email or password!"});
                context.Result.CustomResponse = errors;
                
                return;
            }

            context.Result =
                new GrantValidationResult(isExistUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}