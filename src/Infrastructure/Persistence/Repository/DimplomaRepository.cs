using Application.Features.Dimplomas.Queries.GetPagedList;
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
    public class DimplomaRepository : RepositoryBase<Dimploma>, IDimplomaRepository
    {
        private ISortHelper<Dimploma> _sortHelper;

        public DimplomaRepository
        (
            ApplicationDbContext appDbContext,
            ISortHelper<Dimploma> sortHelper
        ) : base(appDbContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Dimploma>> GetPagedListAsync(GetDimplomasQuery dimplomasQuery)
        {
            var dimplomas = Enumerable.Empty<Dimploma>().AsQueryable();

            ApplyFilters(ref dimplomas, dimplomasQuery);

            PerformSearch(ref dimplomas, dimplomasQuery.SearchTerm);

            var sortedDimplomas = _sortHelper.ApplySort(dimplomas, dimplomasQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Dimploma>.ToPagedList
                (
                    sortedDimplomas,
                    dimplomasQuery.PageNumber,
                    dimplomasQuery.PageSize)
                );
        }


        public async Task<Dimploma> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(dimploma => dimploma.Id.Equals(id))
                .Include(x => x.Students)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Dimploma dimploma)
        {
            return await BaseFindByCondition(x => x.Name == dimploma.Name)
                .AnyAsync();
        }

        public async Task CreateAsync(Dimploma dimploma)
        {
            await BaseCreateAsync(dimploma);
        }

        public async Task UpdateAsync(Dimploma dimploma)
        {
            await BaseUpdateAsync(dimploma);
        }

        public async Task DeleteAsync(Dimploma dimploma)
        {
            await BaseDeleteAsync(dimploma);
        }

        public async Task UpdateAsync(IEnumerable<Dimploma> dimplomas)
        {
            await BaseUpdateAsync(dimplomas);
        }

        public async Task DeleteAsync(IEnumerable<Dimploma> dimplomas)
        {
            await BaseDeleteAsync(dimplomas);
        }

        private void ApplyFilters(ref IQueryable<Dimploma> dimplomas, GetDimplomasQuery dimplomasQuery)
        {
            dimplomas = BaseFindAll();

            /*
            if (dimplomasQuery.MinCreateAt != null)
            {
                dimplomas = dimplomas.Where(x => x.CreateAt >= dimplomasQuery.MinCreateAt);
            }

            if (dimplomasQuery.MaxCreateAt != null)
            {
                dimplomas = dimplomas.Where(x => x.CreateAt < dimplomasQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Dimploma> dimplomas, string searchTerm)
        {
            if (!dimplomas.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            dimplomas = dimplomas.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }


    }
}
