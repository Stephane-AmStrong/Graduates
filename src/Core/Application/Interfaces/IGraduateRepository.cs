using Application.Features.Graduates.Queries.GetPagedList;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGraduateRepository
    {
        Task<PagedList<Graduate>> GetPagedListAsync(GetGraduatesQuery getGraduatesQuery);

        Task<Graduate> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Graduate graduate);

        Task CreateAsync(Graduate graduate);
        Task UpdateAsync(Graduate graduate);
        Task DeleteAsync(Graduate graduate);
        Task UpdateAsync(IEnumerable<Graduate> graduates);
        Task DeleteAsync(IEnumerable<Graduate> graduates);
    }
}
