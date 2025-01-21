
using Microsoft.VisualBasic;

namespace Clinic.Entities.DbSets
{
    public class User : Person
    {

        public ICollection<Appointment> Appointments { get; set; } = null!;

    }
}
