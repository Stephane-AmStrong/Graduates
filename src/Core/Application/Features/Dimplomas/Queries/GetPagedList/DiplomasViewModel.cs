using Domain.Common;

namespace Application.Features.Diplomas.Queries.GetPagedList
{
    public record DiplomasViewModel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
