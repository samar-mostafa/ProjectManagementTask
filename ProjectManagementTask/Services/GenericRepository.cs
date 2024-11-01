using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManagement.Models;

namespace ProjectManagement.Services
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        private readonly DbSet<TEntity> _entities;
        private readonly ProjectManagement.Data.DbContext _context;

        public GenericRepository(ProjectManagement.Data.DbContext dbContext)
        {
            _entities = dbContext.Set<TEntity>();
            _context = dbContext;
        }

        public TEntity GetById(object id, bool getDeleted = false)
        {
            return _entities.Where(e => e.IsDeleted == false).FirstOrDefault(c => c.Id == id.ToString());
        }

        public IEnumerable<TEntity> GetByIdsRange(IEnumerable<object> ids)
        {
            return _entities.Where(c => ids.Contains(c.Id) && c.IsDeleted == false);
        }

        public TEntity GetById(object id, string IncludeProperties, bool getDeleted = false)
        {
            IQueryable<TEntity> query = _entities;
            if (!getDeleted)
                query = query.Where(C => C.IsDeleted == false);

            foreach (var includeProperty in IncludeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (getDeleted == false)
                    query = query.Include(includeProperty).Where(c => c.IsDeleted == false);
                else
                    query = query.Include(includeProperty);
            }
            return query.FirstOrDefault(c => c.Id == id.ToString());
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool getDeleted = false)
        {
            IQueryable<TEntity> query = _entities;
            if (!getDeleted)
                query = query.Where(C => C.IsDeleted == false);

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (getDeleted == false)
                {
                    query = query.Include(includeProperty).Where(c => c.IsDeleted == false);

                }
                else
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

        public void Insert(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
               entity.Id = Guid.NewGuid().ToString();
            _entities.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
               foreach (var entity in entities)
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            _entities.AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.IsDeleted = true;
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            entities.ToList().ForEach(e =>
            {
                e.IsDeleted = true;
            });
        }
    }
}
