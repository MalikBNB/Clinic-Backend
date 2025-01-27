using Clinic.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Prescriptions;
public class PrescriptionsDto
{
    public string Id { get; set; } = string.Empty;
    public string Medication { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string Instructions { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public string MedicalRecordId { get; set; } = string.Empty;
    public string CreatorId { get; set; } = string.Empty;
    public string ModifierId { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
