
using Microsoft.VisualBasic;

namespace Clinic.Entities.DbSets
{
    public class User : BaseEntity
    {
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".ToUpper();
        public string Gendor { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }

        public ICollection<Appointment> Appointments { get; set; }

    }
}
