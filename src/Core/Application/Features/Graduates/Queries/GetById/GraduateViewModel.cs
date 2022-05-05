using Application.Features.Dimplomas.Queries.GetById;
using Application.Features.Dimplomas.Queries.GetPagedList;
using Application.Features.Graduates.Queries.GetPagedList;
using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetPagedList;
using Domain.Common;

namespace Application.Features.Graduates.Queries.GetById
{
    public record GraduateViewModel : AuditableBaseEntity
    {
        public DateTime GraduateAt { get; set; }
        public Guid DimplomaId { get; set; }
        public Guid StudentId { get; set; }

        public virtual DimplomasViewModel Dimploma { get; set; }

        public virtual StudentsViewModel Student { get; set; }
    }
}
