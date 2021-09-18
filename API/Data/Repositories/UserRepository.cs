using API.Data.Entities;
using API.Data.Models;
using GAE.AIQ.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
	public class UserRepository : Repository<User, IDbContext>,IUserRepository
	{
        public readonly IDbContext _context;
        public UserRepository(IDbContext context) : base(context)
		{
            _context = context;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var a = await _context.Set<User>()
                    .Include("Role")
                    .Where(x => x.UserName == userName)
                    .Select(x => new User()
                    {
                        Id = x.Id,
                        FullName = x.FullName,
                        Email = x.Email,
                        Phone = x.Phone,
                        Password = x.Password,
                        UserName = x.UserName,
                        Role=x.Role
                    })
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            return a;
        }
    }
}
