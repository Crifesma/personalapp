using API.Data.Entities;
using GAE.AIQ.API.Data;

namespace API.Data.Repositories
{
    public class RoleRepository : Repository<Role, IDbContext> , IRoleRepository
    {
        public RoleRepository(IDbContext context) : base(context)
        {

        }
    }
}
