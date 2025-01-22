using Clinic.Entities.DTOs.BaseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Outgoing
{
    public class ProfileDto : PersonDto
    {
        public string Gendor { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
