using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Doctors
{
    public class DoctorDto : UserDto
    {
        public string Specialization { get; set; } = string.Empty;
    }
}
