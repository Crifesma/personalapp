using API.Data.Entities;
using API.Data.Models;
using GAE.AIQ.API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
	public class UserRepository : Repository<User, IDbContext>,IUserRepository
	{
        public readonly IDbContext context;
        public UserRepository(IDbContext _context) : base(_context)
		{
            context = _context;
        }

        public async override Task<QueryResult<User>> Get(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 0)
        {
            IQueryable<User> query = context.Set<User>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            var pagedResult = new QueryResult<User>();
            var result = new List<User>();

            if (orderBy != null)
            {
                result = orderBy(query).ToList();
            }
            else
            {
                result = await query.Select(x=>new User() {
                    Id = x.Id,
                    UserName = x.UserName,
                    FullName = x.FullName,
                    Email = x.Email,
                    Address=x.Address,
                    Age=x.Age,
                    Phone = x.Phone,
                    RoleId = x.RoleId
                }).ToListAsync();
            }

            pagedResult.TotalRecords = result.Count();
            pagedResult.CurrentPage = pageIndex + 1;
            pagedResult.PageSize = pageSize;
            pagedResult.Data = result.Skip(pageIndex * pageSize).Take(pageSize).ToList();

            return pagedResult;
        }

        public async Task<User> GetUserByName(string userName)
        {
            var a = await context.Set<User>()
                    .Include("Role")
                    .Where(x => x.UserName == userName)
                    .Select(x => new User()
                    {
                        Id = x.Id,
                        UserName = x.UserName,
                        FullName = x.FullName,
                        Email = x.Email,
                        Phone = x.Phone,
                        Password = x.Password,
                        RoleId = x.RoleId,
                        Role =x.Role
                    })
                    .AsNoTracking()
                    .SingleOrDefaultAsync();
            return a;
        }

        public async override Task<User> Update(User entity)
        {
            if(entity.Password==null || entity.Password == "")
            {
                User loadUser = await Get(entity.Id, "");
                entity.Password = loadUser.Password;
            }

            return await base.Update(entity);
        }
    }
}
