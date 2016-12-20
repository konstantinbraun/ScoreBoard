using System.Collections.Generic;
using IdentitySample.Entities;

namespace IdentitySample.Service
{
    public interface IEntityService<T> : IService
     where T : BaseEntity
    {
        T Create(T entity);
        void Delete(T entity);
        IEnumerable<T> GetAll();      
        void Update(T entity);
    }
}
