using Domain.Common;

namespace Application.Features.Graduates.Queries.GetPagedList
{
    public record GraduatesViewModel : AuditableBaseEntity
    {
        public DateTime GraduateAt { get; set; }
        public Guid DiplomaId { get; set; }
        public Guid StudentId { get; set; }
    }
}
