using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Entities.DbSets
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte Status { get; set; } = 1;
        public DateTime Created {  get; set; } = DateTime.Now;
        public DateTime Modified {  get; set; }
    }
}
