using Clinic.Entities.DbSets;
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
        //Task<User> GetByIdentityIdAsync(Guid identityId, bool isPatient);

    }
}
