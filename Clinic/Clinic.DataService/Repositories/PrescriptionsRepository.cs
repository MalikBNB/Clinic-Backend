using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Prescriptions;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class PrescriptionsRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
            
        }

        public async Task<bool> UpdateAsync(PrescriptionsDto dto)
        {
            var prescription = await dbSet.FindAsync(new Guid(dto.Id));
            if (prescription == null) 
                return false;

            prescription.Medication = dto.Medication;
            prescription.Dosage = dto.Dosage;
            prescription.Frequency = dto.Frequency;
            prescription.Instructions = dto.Instructions;
            prescription.StartDate = dto.StartDate;
            prescription.EndDate = dto.EndDate;
            prescription.ModifierId = dto.ModifierId;
            prescription.Modified = dto.Modified;

            return true;
        }
    }
}
