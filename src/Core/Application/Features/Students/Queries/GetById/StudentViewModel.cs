using Application.Features.Graduates.Queries.GetById;
using Application.Features.Graduates.Queries.GetPagedList;
using Application.Features.Students.Queries.GetPagedList;
using Domain.Common;

namespace Application.Features.Students.Queries.GetById
{
    public record StudentViewModel : AuditableBaseEntity
    {
        public string ImgLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public virtual GraduatesViewModel[] Graduates { get; set; }
    }
}
