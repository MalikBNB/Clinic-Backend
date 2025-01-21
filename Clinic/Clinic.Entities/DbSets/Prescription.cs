using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets.Abstract_classes;

namespace Clinic.Entities.DbSets
{
    public class Prescription : BaseEntity
    {
        public string Medication {  get; set; } = string.Empty;
        public string Dosage {  get; set; } = string.Empty;
        public string Frequency {  get; set; } = string.Empty;
        public string Instructions {  get; set; } = string.Empty;
        public DateTime StartDate {  get; set; } = DateTime.Now;
        public DateTime EndDate {  get; set; }

        public string MedicalRecordId { get; set; } = string.Empty;
        public MedicalRecord MedicalRecord { get; set; } = null!;
    }
}
