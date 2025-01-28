using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;
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
            return await dbSet.FirstOrDefaultAsync(p => p.Id == id && p.Status == 1) ?? null!;
        }

        public override async Task<Patient> GetByIdentityIdAsync(Guid identityId)
        {
            return await dbSet.FirstOrDefaultAsync(d => d.IdentityId == identityId && d.Status == 1) ?? null!;
        }

        public override async Task<Patient> GetByEmailsync(string email)
        {
            return await dbSet.SingleOrDefaultAsync(p => p.Email == email && p.Status == 1) ?? null!;
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateProfileDto dto)
        {
            try
            {
                var userToUpdate = await dbSet.FirstOrDefaultAsync(u => u.Id == id
                                                                     && u.Status == 1);
                if (userToUpdate is null) return false;

                userToUpdate.DateOfBirth = dto.DateOfBirth;
                userToUpdate.Phone = dto.Phone;
                userToUpdate.Address = dto.Address;
                userToUpdate.Gendor = dto.Gendor;
                userToUpdate.Modified = DateTime.Now;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method UpdateAsync has generated an error", typeof(UsersRepository));
                return false;
            }
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await dbSet.FindAsync(id);
            if (patient == null) return false;

            patient.Status = 0;
            patient.Modified = DateTime.Now;

            return true;
        }
    }
}
