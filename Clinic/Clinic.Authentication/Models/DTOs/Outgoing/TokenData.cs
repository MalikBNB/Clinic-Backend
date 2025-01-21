using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authentication.Models.DTOs.Outgoing
{
    public class TokenData
    {
        public string JwtToken {  get; set; } = string.Empty;
        public string RefreshToken {  get; set; } = string.Empty;
    }
}
