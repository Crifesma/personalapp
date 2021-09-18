using GAE.AIQ.API.Data;

namespace API.Data
{
    public interface IDataBaseFactory
    {
        public ApplicationDbContext GetContext();
    }
}
