using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Prescription
    {
        public Guid Id { get; set; }

        public Guid MedicalRecordId { get; set; }
        public MedicalRecord MedicalRecord { get; set; }

        public string Medication {  get; set; } = string.Empty;
        public string Dosage {  get; set; } = string.Empty;
        public string Frequency {  get; set; } = string.Empty;
        public string Instructions {  get; set; } = string.Empty;
        public DateTime StartDate {  get; set; } = DateTime.Now;
        public DateTime EndDate {  get; set; } = DateTime.Now;
    }
}
