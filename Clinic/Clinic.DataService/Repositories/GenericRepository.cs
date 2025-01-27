using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly ILogger _logger;
        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            dbSet = context.Set<T>();
        }

        public virtual async Task<bool> AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity is null)
                return false;

            return await Task.Run(() => Delete(entity));
        }

        public virtual bool Delete(T entity)
        {
            dbSet.Remove(entity);
            return true;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            return await Task.Run(() => Delete(entity));
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await dbSet.FindAsync(id) ?? null!;
        }

        public virtual async Task<T> GetByUserIdAsync(Guid userId)
        {
            return await dbSet.FindAsync(userId) ?? null!;
        }

        public virtual async Task<T> GetByIdentityIdAsync(Guid identityId)
        {
            return await dbSet.FindAsync(identityId) ?? null!;
        }

        public virtual async Task<T> GetByEmailsync(string email)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includes = null!, bool trackObject = false)
        {
            IQueryable<T> query = dbSet;

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (trackObject)
                return await query.ToListAsync();

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null!, bool trackObject = false)
        {
            IQueryable<T> query = dbSet;

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (trackObject)
                return await query.Where(criteria).ToListAsync();

            return await query.AsNoTracking().Where(criteria).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            IQueryable<T> query = dbSet;

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.SingleOrDefaultAsync(criteria) ?? null!;
        }
    }
}
