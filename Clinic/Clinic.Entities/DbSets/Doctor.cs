using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Doctor : Person
    {
        public string Specialization { get; set; } = string.Empty;

        public ICollection<Appointment> Appointments { get; set; } = null!;
    }
}
