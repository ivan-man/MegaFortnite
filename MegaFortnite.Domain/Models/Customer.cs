using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MegaFortnite.Domain.Models
{
    public class Customer : BaseEntity<int>
    {
        [MaxLength(255)] public string FirstName { get; init; }
        [MaxLength(255)] public string LastName { get; init; }
        [MaxLength(32)] public string Phone { get; init; }
        [MaxLength(128)] public string Email { get; init; }

        public List<Profile> Profiles { get; set; }
    }
}
