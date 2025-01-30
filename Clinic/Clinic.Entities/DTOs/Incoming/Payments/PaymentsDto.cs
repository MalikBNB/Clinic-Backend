using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.Entities.DTOs.Incoming.Payments
{
    public class PaymentsDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        [Required]
        [Range(500, 100000000)]
        public decimal Amount { get; set; }

        public string Notes { get; set; } = string.Empty;

        [Required]
        public string AppointmentId { get; set; } = string.Empty;
    }
}
