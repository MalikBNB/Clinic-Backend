using Clinic.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.MedicalRecords;
public class MedicalRecordDto
{
    public string Id { get; set; } = string.Empty;
    public string VisitDescription { get; set; } = string.Empty;
    public string Diagnosis { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public string AppointmentId { get; set; } = string.Empty;
    //public Appointment Appointment { get; set; } = null!;
    public string CreatorId { get; set; } = string.Empty;
    public string ModifierId { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
