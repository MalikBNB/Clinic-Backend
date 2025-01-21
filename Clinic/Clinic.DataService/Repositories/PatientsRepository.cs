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

        public override async Task<IEnumerable<Patient>> GetAllAsync(string[] includes = null, bool trackObject = false)
        {
            return await dbSet.Where(p => p.Status == 1).ToListAsync();
        }

        public override async Task<Patient> GetByIdAsync(Guid id)
        {
            return await dbSet.FirstOrDefaultAsync(p => p.Id == id && p.Status == 1);
        }

        public override async Task<Patient> GetByIdentityIdAsync(Guid identityId)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.IdentityId == identityId && d.Status == 1);
        }

        public override async Task<Patient> GetByEmailsync(string email)
        {
            return await dbSet.SingleOrDefaultAsync(p => p.Email == email && p.Status == 1);
        }
    }
}
