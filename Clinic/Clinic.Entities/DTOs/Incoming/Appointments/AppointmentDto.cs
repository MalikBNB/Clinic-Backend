using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.Entities.DTOs.Incoming.Appointments
{
    public class AppointmentDto
    {
        public DateTime Date { get; set; }
        public AppointmentStatus status { get; set; } = AppointmentStatus.Confirmed;
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid CreatorId { get; set; }
        public DateTime Created { get; set; }
        public Guid ModifierId { get; set; }
        public DateTime Modified { get; set; }
    }
}
