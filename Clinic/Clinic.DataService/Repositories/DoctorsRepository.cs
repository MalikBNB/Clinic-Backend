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
    public class DoctorsRepository : GenericRepository<Doctor>, IDoctorsRepository
    {
        public DoctorsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Doctor>> GetAllAsync(string[] includes = null!, bool trackObject = false)
        {
            try
            {
                return await dbSet.Where(d => d.Status == 1)
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
            try
            { 
                return await dbSet.FirstOrDefaultAsync(d => d.Id == id && d.Status == 1) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByIdAsync has generated an error", typeof(DoctorsRepository));
                return new Doctor();
            }
        }

        public override async Task<Doctor> GetByIdentityIdAsync(Guid identityId)
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync(d => d.IdentityId == identityId && d.Status == 1) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByIdentityIdAsync has generated an error", typeof(DoctorsRepository));
                return new Doctor();
            }
        }

        public async Task<IEnumerable<Doctor>> GetAllBySpeciality(string speciality)
        {
            try
            {
                if (string.IsNullOrEmpty(speciality)) return new List<Doctor>();

                return await dbSet.Where(d => d.Specialization == speciality && d.Status == 1).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetAllBySpeciality has generated an error", typeof(DoctorsRepository));
                return new List<Doctor>();
            }           
        }

        public async Task<bool> IsDoctorExists(string email)
        {
            try
            {
                return await dbSet.SingleOrDefaultAsync(d => d.Email == email) != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method IsDoctorExists has generated an error", typeof(DoctorsRepository));
                return false;
            }
        }

        public async Task<bool> UpdateAsync(UpdateDoctorProfileDto dto)
        {
            try
            {
                var doctorToUpdate = await dbSet.FirstOrDefaultAsync(u => u.Id == new Guid(dto.Id)
                                                                     && u.Status == 1);
                if (doctorToUpdate is null) return false;

                doctorToUpdate.DateOfBirth = dto.DateOfBirth;
                doctorToUpdate.Specialization = dto.Sepecialization;
                doctorToUpdate.Phone = dto.Phone;
                doctorToUpdate.Address = dto.Address;
                doctorToUpdate.Gendor = dto.Gendor;
                doctorToUpdate.Modified = DateTime.Now;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method UpdateAsync has generated an error", typeof(DoctorsRepository));
                return false;
            }
        }

        public override async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var doctor = await dbSet.FindAsync(id);
                if (doctor == null) return false;

                doctor.Status = 0;
                doctor.Modified = DateTime.Now;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method DeleteAsync has generated an error", typeof(DoctorsRepository));
                return false;
            }
        }
    }
}
