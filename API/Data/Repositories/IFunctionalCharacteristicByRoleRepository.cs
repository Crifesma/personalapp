using API.Data.Entities;
using API.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public interface IFunctionalCharacteristicByRoleRepository: IRepository<FunctionalCharacteristicByRole>
    {
        Task<List<FunctionalCharacteristicByRole>> FindElementsForRoleId(int roleId);
        
    }
}
