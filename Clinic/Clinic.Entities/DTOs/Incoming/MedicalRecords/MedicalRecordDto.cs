using Clinic.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.MedicalRecords;
public class MedicalRecordDto
{
    [Required]
    public string VisitDescription { get; set; } = string.Empty;

    [Required]
    public string Diagnosis { get; set; } = string.Empty;

    public string Notes { get; set; } = string.Empty;

    [Required]
    public Guid AppointmentId { get; set; }
}
