using API.Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController :
            EntityControllerBase<Role, IRoleService>
    {
        public RoleController(IRoleService service) : base(service)
        {

        }
    }
  
}
