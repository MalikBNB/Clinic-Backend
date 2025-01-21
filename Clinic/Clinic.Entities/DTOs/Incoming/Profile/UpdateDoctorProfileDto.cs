using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Incoming.Profile
{
    public class UpdateDoctorProfileDto : UpdateProfileDto
    {
        public string Sepecialization { get; set; } = string.Empty;
    }
}
