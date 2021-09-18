using API.Data.Entities;
using API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class RoleService : EntityService<Role, IRoleRepository>, IRoleService
    {
        public RoleService(IRoleRepository _repository) : base(_repository)
        {
        }
    }
}
