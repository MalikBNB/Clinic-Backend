

namespace Clinic.Entities.DbSets
{
    public interface IPerson
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}".ToUpper();
        public string Gendor { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

    }
}
