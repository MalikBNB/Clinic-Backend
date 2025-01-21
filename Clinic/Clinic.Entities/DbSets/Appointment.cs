using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date {  get; set; }
        public AppointmentStatus status { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public MedicalRecord? MedicalRecord { get; set; }

        public Payment? Payment { get; set; }

        public Guid CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public DateTime Created {  get; set; }

        public Guid ModifierId { get; set; }
        public virtual User Modifier { get; set; }
        public DateTime Modified { get; set; }
    }

    public enum AppointmentStatus
    {
        //Pending,
        Confirmed = 1, 
        Completed,
        Canceled,
        Rescheduled,
        NoShow // The patient did not show up for the appointment without canceling or rescheduling.
    }
}
