using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Entities.DTOs.Outgoing
{
    public class AppointmentResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public AppointmentStatus status { get; set; }
        public string PatientId { get; set; } = string.Empty;
        public string Patient { get; set; } = string.Empty;
        public string DoctorId { get; set; } = string.Empty;
        public string Doctor { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public IdentityUser Creator { get; set; } = null!;
        public DateTime Created { get; set; }
        public string ModifierId { get; set; } = string.Empty;
        public IdentityUser Modifier { get; set; } = null!;
        public DateTime Modified { get; set; }
    }
}
