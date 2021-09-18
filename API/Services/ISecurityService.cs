using API.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public interface ISecurityService
    {
        Task<IActionResult> Login(AuthInput authInput);
    }
}
