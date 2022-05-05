using Domain.Common;

namespace Application.Features.Dimplomas.Queries.GetPagedList
{
    public record DimplomasViewModel : AuditableBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
