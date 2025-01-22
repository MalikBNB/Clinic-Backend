using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.Entities.DTOs.Outgoing
{
    public class AppointmentResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public AppointmentStatus status { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public Patient Patient { get; set; } = null!;
        public string DoctorId { get; set; } = string.Empty;
        public Doctor Doctor { get; set; } = null!;
        public string CreatorId { get; set; } = string.Empty;
        public User Creator { get; set; } = null!;
        public DateTime Created { get; set; }
        public string ModifierId { get; set; } = string.Empty;
        public User Modifier { get; set; } = null!;
        public DateTime Modified { get; set; }
    }
}
