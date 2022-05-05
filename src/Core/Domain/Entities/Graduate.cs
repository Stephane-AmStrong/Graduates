using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public record Graduate : AuditableBaseEntity
    {
        [Required]
        public DateTime GraduateAt { get; set; }
        [Required]
        public Guid DiplomaId { get; set; }
        [Required]
        public Guid StudentId { get; set; }

        [ForeignKey("DiplomaId")]
        public virtual Diploma Diploma { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
