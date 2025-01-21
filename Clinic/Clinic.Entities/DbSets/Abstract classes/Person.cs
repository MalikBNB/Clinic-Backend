using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.Entities.DbSets.Abstract_classes;

namespace Clinic.Entities.DbSets
{
    public abstract class Person : BaseEntity
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
        public byte Status { get; set; } = 1;

    }
}
