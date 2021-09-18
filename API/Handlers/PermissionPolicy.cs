using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Handlers
{
    public class PermissionPolicy : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "permissions"))
            {
                return Task.CompletedTask;
            }

            string values = context.User.FindFirst(c => c.Type == "permissions").Value;
            var val = values.ToArray();
            if (val[requirement.Position - 1].ToString() == "1")
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
