using API.Data.Entities;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByName(string userName);
    }
}
