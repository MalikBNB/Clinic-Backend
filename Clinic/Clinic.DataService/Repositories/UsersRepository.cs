using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.Repositories
{
    public class UsersRepository : GenericRepository<User>, IUsersRepository
    {
        public UsersRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }


        public override async Task<IEnumerable<User>> GetAllAsync(string[] includes = null, bool trackObject = false)
        {
            try
            {
                return await dbSet.Where(u => u.Status == 1)
                                    .AsNoTracking()
                                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetAllAsync has generated an error", typeof(UsersRepository));
                return new List<User>();
            }
        }

        public async Task<bool> IsUserExists(string email)
        {
            return await dbSet.SingleOrDefaultAsync(u => u.Email == email) != null;
        }

        public override async Task<User> GetByIdentityIdAsync(Guid identityId)
        {
            try
            {
                return await dbSet.FirstOrDefaultAsync(u => u.IdentityId == identityId
                                                         && u.Status == 1) ?? null!;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method GetByIdentityId has generated an error", typeof(UsersRepository));
                return null!;
            }
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
                //userToUpdate.ModifierId = dto.ModifierId;
                userToUpdate.Modified = DateTime.Now;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Method UpdateAsync has generated an error", typeof(UsersRepository));
                return false;
            }
        }

    }
}
