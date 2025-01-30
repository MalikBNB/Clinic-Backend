using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Profile
{
    public class UpdateDoctorProfileDto : UpdateProfileDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Sepecialization cannot be less than 5 characters")]
        [MaxLength(350, ErrorMessage = "Sepecialization cannot be more than 350 characters")]
        public string Sepecialization { get; set; } = string.Empty;
    }
}
