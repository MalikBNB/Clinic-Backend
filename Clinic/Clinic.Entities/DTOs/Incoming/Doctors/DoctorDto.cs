using Clinic.Entities.DTOs.BaseDtos;
using Clinic.Entities.DTOs.Incoming.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Doctors
{
    public class DoctorDto : PersonDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Specialization cannot be over 150 characters")]
        public string Specialization { get; set; } = string.Empty;
    }
}
