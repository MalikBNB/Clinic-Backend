﻿

namespace Clinic.Entities.Global.Generic
{
    public class Result<T> where T : class
    {
        public T Content { get; set; } = null!;
        public Error Error { get; set; } = null!;
        public bool IsSuccess => Error == null;
        public DateTime ResponseDate { get; set; } = DateTime.Now;
    }
}
