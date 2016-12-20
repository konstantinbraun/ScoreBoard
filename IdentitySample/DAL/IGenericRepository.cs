using IdentitySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace IdentitySample.DAL
{
    public  interface IGenericRepository<T> 
        where T : class
    {
        IEnumerable<T> GetById();
        IEnumerable<T> GetWithRawSql();
        IEnumerable<T> Get( Expression<Func<T, bool>> filter = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                            string includeProperties = "");
        T Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
