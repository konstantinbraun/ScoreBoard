using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Linq.Expressions;
using IdentitySample.Models;
using System.Web.Mvc;

namespace IdentitySample.DAL
{
    public class GenericRepository<TEntity> 
        where TEntity : class
    {
        internal ScoreSystemDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ScoreSystemDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        //public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        //{
        //    return dbSet.SqlQuery(query, parameters).ToList();
        //}
        
        public void GetWithRawSql(string query, params object[] parameters)
        {
            dbSet.SqlQuery(query, parameters);
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            return dbSet.Add(entity);
        }

        public virtual TEntity Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            return Delete(entityToDelete);
        }

        public virtual TEntity Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            return dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}
