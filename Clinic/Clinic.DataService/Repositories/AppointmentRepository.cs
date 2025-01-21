using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataService.Data;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public async Task<Appointment> FindAsync(Guid id, string[] includes = null)
        {
            IQueryable<Appointment> query = dbSet;

            if(includes != null)
                foreach(var include in includes)
                    query = query.Include(include);

            return await query.Where(o => o.Id == id).SingleOrDefaultAsync();
        }


    }
}
