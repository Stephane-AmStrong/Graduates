using Domain.Common;

namespace Application.Features.Students.Queries.GetPagedList
{
    public record StudentsViewModel : AuditableBaseEntity
    {
        //public string ImgLink { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }
}
