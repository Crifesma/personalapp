using API.Data.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :
            EntityControllerBase<User, IUserService>
    {
        public UserController(IUserService service) : base(service)
        {

        }
    }
}
