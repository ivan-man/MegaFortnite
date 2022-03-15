using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaFortnite.Domain.Models
{
    public class Profile : BaseEntity<int>
    {
        [MaxLength(128)] public string NickName { get; init; }
        public Guid CustomerId { get; init; }
        public decimal WinRate { get; set; }
        public int Rate { get; set; }
        public List<Session> Sessions { get; init; }
    }
}
