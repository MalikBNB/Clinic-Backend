using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Doctor
    {
        public Guid Id { get; set; }

        public string Specialization { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
    }
}
