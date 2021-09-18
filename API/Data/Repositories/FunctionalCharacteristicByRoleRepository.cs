using API.Data.Entities;
using API.Data.Models;
using GAE.AIQ.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System;

namespace API.Data.Repositories
{
    public class FunctionalCharacteristicByRoleRepository : Repository<FunctionalCharacteristicByRole, IDbContext>,IFunctionalCharacteristicByRoleRepository
    {
        public readonly IDbContext context;
        public FunctionalCharacteristicByRoleRepository(IDbContext _context) : base(_context)
        {
            context = _context;
        }

        public async Task<List<FunctionalCharacteristicByRole>> FindElementsForRoleId(int roleId)
        {
            return await context.Set<FunctionalCharacteristicByRole>().
                Where(x => x.RoleId.Equals(roleId))
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
