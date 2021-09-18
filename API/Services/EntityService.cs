using API.Data;
using API.Data.Entities;
using API.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace API.Services
{
    public class EntityService<T, TRepository> : IService<T>
        where T : class, IEntity
        where TRepository : IRepository<T>
    {
        private readonly TRepository repository;

        public EntityService(TRepository _repository)
        {
            repository = _repository;
        }
        public Task<T> Add(T entity)
        {
            return repository.Add(entity);
        }

        public Task<T> Delete(int id)
        {
            return repository.Delete(id);
        }

        public Task<T> Get(int id, string includeProperties = "")
        {
            return repository.Delete(id);
        }


        public Task<QueryResult<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int pageSize = 10, int pageIndex = 0)
        {
            return repository.Get(filter, orderBy, includeProperties, pageSize, pageIndex);

        }

        public Task<List<T>> GetAll()
        {
            return repository.GetAll();
        }

        public Task<T> Update(T entity)
        {
            return repository.Update(entity);
        }
    }
}
