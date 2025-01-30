using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.MedicalRecords;

namespace Clinic.DataService.IRepositories
{
    public interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {

    }
}
