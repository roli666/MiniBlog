using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using MiniBlog.Core.Constants;
using MiniBlog.IdentityServer.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniBlog.IdentityServer
{
    public class MinimumRoleHandler : AuthorizationHandler<MinimumRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRoleRequirement requirement)
        {
            var roleMap = new Dictionary<string, int> {
                {Roles.Standard,1 },
                {Roles.Admin,2 }
            };

            var role = context.User.FindFirst(c => c.Type == JwtClaimTypes.Role);
            if (role == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (roleMap[role.Value] >= roleMap[requirement.MinimumRole])
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
