using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
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
