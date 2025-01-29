using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets;

namespace Clinic.Entities.DTOs.Outgoing
{
    public class PaymentsResponseDto
    {
        public string Id { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string AppointmentId { get; set; } = string.Empty;
        public string AppointmentDate { get; set; } = string.Empty;
        public string Patient { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public string ModifierId { get; set; } = string.Empty;
        public string Modifier { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
