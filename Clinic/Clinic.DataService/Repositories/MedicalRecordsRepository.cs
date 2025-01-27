
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

        public async Task<bool> UpdateAsync(MedicalRecordDto dto)
        {
            var oldRecord = await dbSet.FindAsync(dto.Id);
            if (oldRecord is null)
                return false;

            oldRecord.VisitDescription = dto.VisitDescription;
            oldRecord.Diagnosis = dto.Diagnosis;
            oldRecord.Notes = dto.Notes;
            oldRecord.Modified = dto.Modified;
            oldRecord.ModifierId = dto.ModifierId;

            return true;
        }
    }
}
