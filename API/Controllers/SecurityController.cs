using API.Data.Models;
using API.Data.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService securityService;
        public SecurityController(ISecurityService _securityService)
        {
            securityService = _securityService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthInput authInput)
        {
            return await securityService.Login(authInput);
        }

    }
}
