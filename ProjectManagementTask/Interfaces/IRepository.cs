using ProjectManagement.Models;
using System.Linq.Expressions;

namespace ProjectManagement.Services
{
    public interface IRepository<TEntity> where TEntity : BaseModel
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
           string includeProperties = "", bool getDeleted = false);

        TEntity GetById(object id, bool getDeleted = false);
        TEntity GetById(object id, string IncludeProperties, bool getDeleted = false);
        IEnumerable<TEntity> GetByIdsRange(IEnumerable<object> ids);

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void Delete(IEnumerable<TEntity> entities);
    }
}
