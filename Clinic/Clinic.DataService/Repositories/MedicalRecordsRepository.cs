
using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.MedicalRecords;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class MedicalRecordsRepository : GenericRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        
    }
}
