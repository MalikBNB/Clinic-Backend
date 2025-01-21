using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authentication.Models.DTOs.Incoming
{
    public class DoctorRegistrationRequestDto : UserRegistrationRequestDto
    {
        [Required]
        public string Specialization {  get; set; } = string.Empty;
    }
}
