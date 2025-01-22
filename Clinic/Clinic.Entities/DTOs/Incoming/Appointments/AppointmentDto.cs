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
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public AppointmentStatus status { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string ModifierId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
