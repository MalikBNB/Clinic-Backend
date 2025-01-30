using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.Entities.DTOs.Incoming.Appointments
{
    public class AppointmentDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public AppointmentStatus status { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid DoctorId { get; set; }
    }
}
