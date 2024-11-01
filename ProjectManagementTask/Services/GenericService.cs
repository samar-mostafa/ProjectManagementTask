using ProjectManagement.Models;
using System.Linq.Expressions;

namespace ProjectManagement.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseModel
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;



        public GenericService(IRepository<TEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public TEntity? GetById(string id, bool getDeleted = false)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return _repository.GetById(id, getDeleted);
        }

        public TEntity? GetById(string id, string includeProperties, bool getDeleted = false)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return _repository.GetById(id, includeProperties, getDeleted);
        }

        public IEnumerable<TEntity> GetByIdsRange(IEnumerable<string> ids)
        {
            if (ids == null)
                return null;
            return _repository.GetByIdsRange(ids);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool getDeleted = false)
        {
            return _repository.Get(filter, orderBy, includeProperties, getDeleted);
        }

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            _repository.Insert(entity);
            _unitOfWork.Complete();
        }


        public void Insert(IEnumerable<TEntity> peojects)
        {
            if (peojects == null || peojects.Count() == 0)
                throw new ArgumentNullException(nameof(peojects), "list of entity cannot be null or empty");
            _repository.Insert(peojects);
            _unitOfWork.Complete();
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.UpdatedAt = DateTime.Now;

            _unitOfWork.Complete();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _repository.Delete(entity);
            _unitOfWork.Complete();
        }

        public void Delete(IEnumerable<TEntity> entites)
        {
            if (entites == null || entites.Count() == 0)
                throw new ArgumentNullException(nameof(entites), "list of entity cannot be null or empty");

            _repository.Delete(entites);
            _unitOfWork.Complete();
        }
    }
}
