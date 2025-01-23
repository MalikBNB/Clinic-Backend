using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Clinic.Entities.DbSets.Abstract_classes
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatorId { get; set; } = string.Empty;
        public string ModifierId { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public IdentityUser Creator { get; set; } = null!;
        public IdentityUser Modifier { get; set; } = null!;
    }
}
