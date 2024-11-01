using ProjectManagement.Models;
using System.Linq.Expressions;

namespace ProjectManagement.Services
{
    public interface IGenericService<TEntity> where TEntity : BaseModel
    {
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", bool getDeleted = false);
        TEntity? GetById(string id, bool getDeleted = false);
        TEntity? GetById(string id, string includeProperties, bool getDeleted = false);
        IEnumerable<TEntity> GetByIdsRange(IEnumerable<string> ids);

        void Insert(IEnumerable<TEntity> peojects);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(IEnumerable<TEntity> entites);
        void Delete(TEntity entity);
    }
}
