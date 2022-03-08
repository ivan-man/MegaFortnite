using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaFortnite.Domain.Models
{
    public abstract class BaseEntity<TId> : BaseEntity
    {
        [Key]
        public TId Id { get; init; }
    }

    public abstract class BaseEntity : IBaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
