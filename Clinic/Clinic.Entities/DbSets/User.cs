
using Microsoft.VisualBasic;

namespace Clinic.Entities.DbSets
{
    public class User : IPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid IdentityId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".ToUpper();
        public string Gendor { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public byte Status { get; set; } = 1;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = null!;
    }
}
