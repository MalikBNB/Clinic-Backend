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
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method AddAsync has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                await Task.Run(() => { dbSet.Update(entity); });
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method UpdateAsync has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await dbSet.FindAsync(id);

                if (entity is null)
                    return false;

                return await Task.Run(() => Delete(entity));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method DeleteAsync has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            try
            {
                dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method Delete has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            try
            { 
                return await Task.Run(() => Delete(entity)); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method DeleteAsync has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            try
            {
                return await dbSet.FindAsync(id) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByIdAsync has generated an error", typeof(GenericRepository<T>));
                return null!;
            }
        }

        public virtual async Task<T> GetByUserIdAsync(Guid userId)
        {
            try
            {
                return await dbSet.FindAsync(userId) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByUserIdAsync has generated an error", typeof(GenericRepository<T>));
                return null!;
            }
        }

        public virtual async Task<T> GetByIdentityIdAsync(Guid identityId)
        {
            try
            { 
                return await dbSet.FindAsync(identityId) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByIdentityIdAsync has generated an error", typeof(GenericRepository<T>));
                return null!;
            }
        }

        public virtual async Task<T> GetByEmailsync(string email)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string[] includes = null!, bool trackObject = false)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (includes is not null)
                    foreach (var include in includes)
                        query = query.Include(include);

                if (trackObject)
                    return await query.ToListAsync();

                return await query.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetAllAsync has generated an error", typeof(GenericRepository<T>));
                return new List<T>();
            }
        }

        public virtual async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null!, bool trackObject = false)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (includes is not null)
                    foreach (var include in includes)
                        query = query.Include(include);

                if (trackObject)
                    return await query.Where(criteria).ToListAsync();

                return await query.AsNoTracking().Where(criteria).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetAllAsync has generated an error", typeof(GenericRepository<T>));
                return new List<T>();
            }
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null!)
        {
            try
            {
                IQueryable<T> query = dbSet;

                if (includes is not null)
                    foreach (var include in includes)
                        query = query.Include(include);

                return await query.SingleOrDefaultAsync(criteria) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method FindAsync has generated an error", typeof(GenericRepository<T>));
                return null!;
            }
        }

        public virtual bool Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method Update has generated an error", typeof(GenericRepository<T>));
                return false;
            }
        }
    }
}
