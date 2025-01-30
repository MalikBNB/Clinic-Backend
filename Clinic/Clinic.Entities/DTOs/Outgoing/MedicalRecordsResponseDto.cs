using Clinic.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Outgoing;
public class MedicalRecordsResponseDto
{
    public string Id { get; set; } = string.Empty;
    public string VisitDescription { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public Guid AppointmentId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string CreatorId { get; set; } = string.Empty;
    public string ModifierId { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
