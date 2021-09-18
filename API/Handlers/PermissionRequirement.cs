using Microsoft.AspNetCore.Authorization;

namespace API.Handlers
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public int Position { get; set; }
        public PermissionRequirement(int position)
        {
            Position = position;
        }

        
    }
}
