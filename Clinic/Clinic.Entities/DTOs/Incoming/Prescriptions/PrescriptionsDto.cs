using Clinic.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Prescriptions;
public class PrescriptionsDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Medication cannot be less than 2 characters")]
    [MaxLength(350, ErrorMessage = "Medication cannot be more than 50 characters")]
    public string Medication { get; set; } = string.Empty;

    [Required]
    public string Dosage { get; set; } = string.Empty;

    [Required]
    public string Frequency { get; set; } = string.Empty;

    [Required]
    public string Instructions { get; set; } = string.Empty;

    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public Guid MedicalRecordId { get; set; }
}
