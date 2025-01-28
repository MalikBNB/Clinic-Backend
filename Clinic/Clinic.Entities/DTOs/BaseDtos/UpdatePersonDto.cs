using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.BaseDtos;
public class UpdatePersonDto
{
    public string Id { get; set; } = string.Empty;
    //public string FirstName { get; set; } = string.Empty;
    //public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Gendor { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    //public string ModifierId { get; set; } = string.Empty;
    public DateTime Modified { get; set; }
}
