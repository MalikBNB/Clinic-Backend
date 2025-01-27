using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.DataService.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync(string[] includes = null, bool trackObject = false);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null, bool trackObject = false);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByUserIdAsync(Guid userId);
        Task<T> GetByIdentityIdAsync(Guid identityId);
        Task<T> GetByEmailsync(string email);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAsync(T entity);
        bool Delete(T entity);
    }
}
