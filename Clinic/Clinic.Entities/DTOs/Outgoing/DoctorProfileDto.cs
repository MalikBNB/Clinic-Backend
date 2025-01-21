using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DTOs.Outgoing
{
    public class DoctorProfileDto : ProfileDto
    {
        public string Specialization {  get; set; } = string.Empty;
    }
}
