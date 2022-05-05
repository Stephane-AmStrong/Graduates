using Application.Features.Students.Queries.GetById;
using Application.Features.Students.Queries.GetPagedList;
using Domain.Common;

namespace Application.Features.Diplomas.Queries.GetById
{
    public record DiplomaViewModel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual StudentsViewModel[] Students { get; set; }
    }
}
