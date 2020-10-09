using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using MiniBlog.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiniBlog.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> mUserManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory;

        public ProfileService(UserManager<ApplicationUser> userManager,IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            mUserManager = userManager;
            this.claimsFactory = claimsFactory;
        }
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            List<string> list = context.RequestedClaimTypes.ToList();
            context.IssuedClaims.AddRange(roleClaims);
            return Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await mUserManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
