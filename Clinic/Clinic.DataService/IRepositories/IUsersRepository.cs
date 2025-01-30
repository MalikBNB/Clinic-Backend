using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataService.IRepositories
{
    public interface IUsersRepository : IGenericRepository<User>
    {
        Task<bool> IsUserExists(string email);
        //Task<bool> UpdateAsync(Guid id, UpdateProfileDto dto);

    }
}
