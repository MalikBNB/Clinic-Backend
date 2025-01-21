using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Authentication.Models.DTOs.Outgoing
{
    public class AuthResult
    {
        public string Token {  get; set; } = string.Empty;
        public string RefreshToken {  get; set; } = string.Empty;
        public bool Success {  get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
