using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Profile;

namespace Clinic.DataService.IRepositories
{
    public interface IPatientsRepository : IGenericRepository<Patient>
    {
        Task<bool> UpdateAsync(Guid id, UpdateProfileDto dto);
    }
}
