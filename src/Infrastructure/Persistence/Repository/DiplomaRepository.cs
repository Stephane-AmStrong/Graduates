using Application.Features.Diplomas.Queries.GetPagedList;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class DiplomaRepository : RepositoryBase<Diploma>, IDiplomaRepository
    {
        private ISortHelper<Diploma> _sortHelper;

        public DiplomaRepository
        (
            ApplicationDbContext appDbContext,
            ISortHelper<Diploma> sortHelper
        ) : base(appDbContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Diploma>> GetPagedListAsync(GetDiplomasQuery diplomasQuery)
        {
            var diplomas = Enumerable.Empty<Diploma>().AsQueryable();

            ApplyFilters(ref diplomas, diplomasQuery);

            PerformSearch(ref diplomas, diplomasQuery.SearchTerm);

            var sortedDiplomas = _sortHelper.ApplySort(diplomas, diplomasQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Diploma>.ToPagedList
                (
                    sortedDiplomas,
                    diplomasQuery.PageNumber,
                    diplomasQuery.PageSize)
                );
        }


        public async Task<Diploma> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(diploma => diploma.Id.Equals(id))
                .Include(x => x.Students)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Diploma diploma)
        {
            return await BaseFindByCondition(x => x.Name == diploma.Name)
                .AnyAsync();
        }

        public async Task CreateAsync(Diploma diploma)
        {
            await BaseCreateAsync(diploma);
        }

        public async Task UpdateAsync(Diploma diploma)
        {
            await BaseUpdateAsync(diploma);
        }

        public async Task DeleteAsync(Diploma diploma)
        {
            await BaseDeleteAsync(diploma);
        }

        public async Task UpdateAsync(IEnumerable<Diploma> diplomas)
        {
            await BaseUpdateAsync(diplomas);
        }

        public async Task DeleteAsync(IEnumerable<Diploma> diplomas)
        {
            await BaseDeleteAsync(diplomas);
        }

        private void ApplyFilters(ref IQueryable<Diploma> diplomas, GetDiplomasQuery diplomasQuery)
        {
            diplomas = BaseFindAll();

            /*
            if (diplomasQuery.MinCreateAt != null)
            {
                diplomas = diplomas.Where(x => x.CreateAt >= diplomasQuery.MinCreateAt);
            }

            if (diplomasQuery.MaxCreateAt != null)
            {
                diplomas = diplomas.Where(x => x.CreateAt < diplomasQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Diploma> diplomas, string searchTerm)
        {
            if (!diplomas.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            diplomas = diplomas.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }


    }
}
