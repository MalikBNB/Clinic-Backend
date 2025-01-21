

namespace Clinic.Entities.Global
{
    public class Error
    {
        public int Code { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
