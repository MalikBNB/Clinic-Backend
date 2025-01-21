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
    public class PatientsRepository : GenericRepository<Patient>, IPatientsRepository
    {
        public PatientsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Patient>> GetAllAsync(string[] includes = null)
        {
            return await dbSet.Include(p => p.User).ToListAsync();
        }

        public override async Task<Patient> GetByIdAsync(Guid id)
        {
            return await dbSet.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<Patient> GetByUserIdAsync(Guid userId)
        {
            return await dbSet.Include(d => d.User)
                              .FirstOrDefaultAsync(d => d.User.Id == userId && d.User.Status == 1);
        }

        public override async Task<Patient> GetByIdentityIdAsync(Guid identityId)
        {
            return await dbSet.Include(d => d.User)
                              .FirstOrDefaultAsync(d => d.User.IdentityId == identityId && d.User.Status == 1);
        }

        public override async Task<Patient> GetByEmailsync(string email)
        {
            return await dbSet.SingleOrDefaultAsync(p => p.User.Email == email && p.User.Status == 1);
        }
    }
}
