using API.Data.Entities;
using API.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class UserService : EntityService<User, IUserRepository>, IUserService
    {
        public UserService(IUserRepository _repository) : base(_repository)
        {
        }
    }
}
