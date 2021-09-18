using API.Data.Entities;
using GAE.AIQ.API.Data;

namespace API.Data.Repositories
{
    public class FunctionalCharacteristicRepository : Repository<FunctionalCharacteristic, IDbContext>, IFunctionalCharacteristicRepository
    {
        public FunctionalCharacteristicRepository(IDbContext context) : base(context)
        {

        }
    }
}
