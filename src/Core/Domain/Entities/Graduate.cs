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
        public Guid DimplomaId { get; set; }
        [Required]
        public Guid StudentId { get; set; }

        [ForeignKey("DimplomaId")]
        public virtual Dimploma Dimploma { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student Student { get; set; }
    }
}
