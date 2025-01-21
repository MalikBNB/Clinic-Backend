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
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus status { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid CreatorId { get; set; }
        public User Creator { get; set; }
        public DateTime Created { get; set; }
        public Guid ModifierId { get; set; }
        public User Modifier { get; set; }
        public DateTime Modified { get; set; }
    }
}
