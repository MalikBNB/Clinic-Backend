using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets.Abstract_classes;

namespace Clinic.Entities.DbSets
{
    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; } = string.Empty;

        public string AppointmentId { get; set; } = string.Empty;
        public Appointment Appointment { get; set; } = null!;
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        Check
    }
}
