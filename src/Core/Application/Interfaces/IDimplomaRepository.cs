using Application.Features.Dimplomas.Queries.GetPagedList;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDimplomaRepository
    {
        Task<PagedList<Dimploma>> GetPagedListAsync(GetDimplomasQuery getDimplomasQuery);

        Task<Dimploma> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Dimploma dimploma);

        Task CreateAsync(Dimploma dimploma);
        Task UpdateAsync(Dimploma dimploma);
        Task DeleteAsync(Dimploma dimploma);
        Task UpdateAsync(IEnumerable<Dimploma> dimplomas);
        Task DeleteAsync(IEnumerable<Dimploma> dimplomas);
    }
}
