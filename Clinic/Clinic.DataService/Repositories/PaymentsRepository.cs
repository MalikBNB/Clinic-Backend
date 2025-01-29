using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
            
        }
    }
}
