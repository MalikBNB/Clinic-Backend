using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authentication.Configuration
{
    public class JwtConfig
    {
        public static string SectionName { get; set; } = "JwtConfig";
        public string Secret { get; set; } = string.Empty;
        public TimeSpan ExpiryTime { get; set; }
    }
}
