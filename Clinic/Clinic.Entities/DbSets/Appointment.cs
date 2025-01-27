using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets.Abstract_classes;

namespace Clinic.Entities.DbSets
{
    public class Appointment : BaseEntity
    {
        public DateTime Date {  get; set; }
        public AppointmentStatus status { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public MedicalRecord? MedicalRecord { get; set; }
        public Payment? Payment { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending = 1,
        Confirmed, 
        Completed,
        Canceled,
        Rescheduled,
        NoShow // The patient did not show up for the appointment without canceling or rescheduling.
    }
}
