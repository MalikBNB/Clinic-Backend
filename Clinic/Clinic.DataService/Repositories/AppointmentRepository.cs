using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataService.Data;
using Clinic.DataService.IConfiguration;
using Clinic.DataService.IRepositories;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Appointments;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Clinic.DataService.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }

        //public async Task<Appointment> FindAsync(Guid id, string[] includes = null)
        //{
        //    IQueryable<Appointment> query = dbSet;

        //    if(includes != null)
        //        foreach(var include in includes)
        //            query = query.Include(include);

        //    return await query.SingleOrDefaultAsync(o => o.Id == id) ?? null!;
        //}

        public override async Task<Appointment> FindAsync(Expression<Func<Appointment, bool>> criteria, string[] includes = null!)
        {
            IQueryable<Appointment> query = dbSet;

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(criteria) ?? null!;
        }

        public async Task<bool> UpdateAsync(AppointmentDto dto)
        {
            var oldAppointment = await dbSet.SingleOrDefaultAsync(a => a.Id == new Guid(dto.Id)
                                                                 && (a.status != AppointmentStatus.Canceled || a.status != AppointmentStatus.Completed));

            if (oldAppointment is null) 
                return false;

            oldAppointment.Date = dto.Date;
            oldAppointment.status = AppointmentStatus.Rescheduled;
            oldAppointment.ModifierId = dto.ModifierId;
            oldAppointment.Modified = dto.Modified;

            return true;
        }
    }
}
