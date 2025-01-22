using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets.Abstract_classes;

namespace Clinic.Entities.DbSets
{
    public class MedicalRecord : BaseEntity
    {
        public string VisitDescription { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;

        public Prescription? Prescription { get; set; }
    }
}
