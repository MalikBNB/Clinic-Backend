using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;
using Clinic.Entities.DTOs.Incoming.Appointments;

namespace Clinic.DataService.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        //Task<Appointment> FindAsync(Guid id, string[] includes = null);
        //Task<Appointment> FindAsync(Expression<Func<Appointment, bool>> criteria, string[] includes = null);
        Task<bool> UpdateAsync(AppointmentDto dto);
    }
}
