using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaFortnite.Domain.Models
{
    public interface IBaseEntity
    {
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}
