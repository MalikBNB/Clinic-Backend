using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.DataService.IRepositories
{
    public interface IPatientsRepository : IGenericRepository<Patient>
    {
    }
}
