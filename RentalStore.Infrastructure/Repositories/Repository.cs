using Microsoft.EntityFrameworkCore;
using RentalStore.Domain.Interfaces;
using System.Linq.Expressions;

namespace RentalStore.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Set<TEntity>().Count();
        }

        public TEntity Get(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IList<TEntity> GetAll()
        {
            return _context.Set<TEntity>()
                .AsNoTracking()
                .ToList();
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>()
                .Where(expression)
                .AsNoTracking()
                .ToList();
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity) 
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
