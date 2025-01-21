using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class MedicalRecord
    {
        public Guid Id { get; set; }
        public string VisitDescription { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;

        public Guid AppointmentId { get; set; }
        public virtual Appointment? Appointment { get; set; }

        public virtual Prescription? Prescription { get; set; }
    }
}
