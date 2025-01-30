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

    }
}
