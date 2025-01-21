using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class DoctorsRepository : GenericRepository<Doctor>, IDoctorsRepository
    {
        public DoctorsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Doctor>> GetAllAsync(string[] includes = null)
        {
            try
            {
                return await dbSet.Include(p => p.User)
                                .Where(d => d.User.Status == 1)
                                .AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Methode GetAllAsync has generated an error", typeof(DoctorsRepository));
                return new List<Doctor>();
            }
        }

        public override async Task<Doctor> GetByIdAsync(Guid id)
        {
            return await dbSet.Include(p => p.User)
                              .FirstOrDefaultAsync(d => d.Id == id && d.User.Status == 1);
        }

        public override async Task<Doctor> GetByIdentityIdAsync(Guid identityId)
        {
            return await dbSet.Include(d => d.User)
                              .FirstOrDefaultAsync(d => d.User.IdentityId == identityId && d.User.Status == 1);
        }

        public override async Task<Doctor> GetByUserIdAsync(Guid userId)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.UserId == userId && d.User.Status == 1);
        }

        public async Task<IEnumerable<Doctor>> GetBySpeciality(string speciality)
        {
            if (string.IsNullOrEmpty(speciality)) return new List<Doctor>();

            return await dbSet.Include(p => p.User)
                              .Where(d => d.Specialization == speciality && d.User.Status == 1).ToListAsync();
        }

        public async Task<bool> IsDoctorExists(string email)
        {
            return await dbSet.SingleOrDefaultAsync(d => d.User.Email == email) != null;
        }
    }
}
