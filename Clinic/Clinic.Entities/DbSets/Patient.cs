using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public class Patient : Person
    {

        public ICollection<Appointment> Appointments { get; set; } = null!;

    }
}
