using Application.Features.Diplomas.Queries.GetPagedList;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDiplomaRepository
    {
        Task<PagedList<Diploma>> GetPagedListAsync(GetDiplomasQuery getDiplomasQuery);

        Task<Diploma> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Diploma diploma);

        Task CreateAsync(Diploma diploma);
        Task UpdateAsync(Diploma diploma);
        Task DeleteAsync(Diploma diploma);
        Task UpdateAsync(IEnumerable<Diploma> diplomas);
        Task DeleteAsync(IEnumerable<Diploma> diplomas);
    }
}
