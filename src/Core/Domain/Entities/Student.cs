using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public record Student : AuditableBaseEntity
    {
        public Student()
        {
            Graduates = new HashSet<Graduate>();
        }

        //public string ImgLink { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Graduate> Graduates { get; set; }
    }
}
