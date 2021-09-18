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
        private readonly IValueEncript valueEncript;
        public UserService(IUserRepository _repository, IValueEncript _valueEncript) : base(_repository)
        {
            valueEncript = _valueEncript;
        }

        public override Task<User> Add(User entity)
        {

            string desencriptPaswword = valueEncript.Desencrypt(entity.Password);
            entity.Password = valueEncript.Sha(desencriptPaswword);

            return base.Add(entity);
        }

        public override Task<User> Update(User entity)
        {
            if (entity.Password != null && entity.Password != "")
            {
                string desencriptPaswword = valueEncript.Desencrypt(entity.Password);
                entity.Password = valueEncript.Sha(desencriptPaswword);
            }
            return base.Update(entity);
        }
    }
}
