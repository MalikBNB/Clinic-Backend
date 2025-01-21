

namespace Clinic.Entities.Global.Generic
{
    public class PagedResult<T> : Result<List<T>>
    {
        public int Page {  get; set; } // page number
        public int ResultCount { get; set; } // total result
        public int ResultPerPage { get; set; } // total result per page
    }
}
