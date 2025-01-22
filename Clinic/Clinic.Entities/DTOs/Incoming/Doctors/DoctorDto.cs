using Clinic.Entities.DTOs.BaseDtos;
using Clinic.Entities.DTOs.Incoming.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Doctors
{
    public class DoctorDto : PersonDto
    {
        public string Specialization { get; set; } = string.Empty;
    }
}
