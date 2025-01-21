using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }

        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        Check
    }
}
