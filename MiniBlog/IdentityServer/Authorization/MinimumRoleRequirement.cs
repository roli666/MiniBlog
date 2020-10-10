using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.IdentityServer.Authorization
{
    public class MinimumRoleRequirement : IAuthorizationRequirement
    {
        public string MinimumRole { get; }
        public MinimumRoleRequirement(string role)
        {
            MinimumRole = role;
        }
    }
}
