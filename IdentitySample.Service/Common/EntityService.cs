using System;
using System.Collections.Generic;
using IdentitySample.Entities;
using IdentitySample.Repository;

namespace IdentitySample.Service
{
    public abstract class EntityService<T> : IEntityService<T> 
        where T : BaseEntity
    {
        IUnitOfWork _unitOfWork;
        IGenericRepository<T> _repository;
        private IUnitService unitOfWork;
        private IFightRelationRepository fightRelationRepository;

        public EntityService(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public virtual T Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            T newEntity =_repository.Add(entity);
            _unitOfWork.Commit();
            return newEntity;
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Edit(entity);
            _unitOfWork.Commit();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _repository.Delete(entity);
            _unitOfWork.Commit();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
