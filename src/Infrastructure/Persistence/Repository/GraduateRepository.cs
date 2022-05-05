using Application.Features.Graduates.Queries.GetPagedList;
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
    public class GraduateRepository : RepositoryBase<Graduate>, IGraduateRepository
    {
        private ISortHelper<Graduate> _sortHelper;

        public GraduateRepository
        (
            ApplicationDbContext appDbContext,
            ISortHelper<Graduate> sortHelper
        ) : base(appDbContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Graduate>> GetPagedListAsync(GetGraduatesQuery graduatesQuery)
        {
            var graduates = Enumerable.Empty<Graduate>().AsQueryable();

            ApplyFilters(ref graduates, graduatesQuery);

            PerformSearch(ref graduates, graduatesQuery.SearchTerm);

            var sortedGraduates = _sortHelper.ApplySort(graduates, graduatesQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Graduate>.ToPagedList
                (
                    sortedGraduates,
                    graduatesQuery.PageNumber,
                    graduatesQuery.PageSize)
                );
        }


        public async Task<Graduate> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(graduate => graduate.Id.Equals(id))
                .Include(x => x.Dimploma)
                .Include(x => x.Student)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Graduate graduate)
        {
            return await BaseFindByCondition(x => x.DimplomaId == graduate.DimplomaId && x.StudentId == graduate.StudentId)
                .AnyAsync();
        }

        public async Task CreateAsync(Graduate graduate)
        {
            await BaseCreateAsync(graduate);
        }

        public async Task UpdateAsync(Graduate graduate)
        {
            await BaseUpdateAsync(graduate);
        }

        public async Task DeleteAsync(Graduate graduate)
        {
            await BaseDeleteAsync(graduate);
        }

        public async Task UpdateAsync(IEnumerable<Graduate> graduates)
        {
            await BaseUpdateAsync(graduates);
        }

        public async Task DeleteAsync(IEnumerable<Graduate> graduates)
        {
            await BaseDeleteAsync(graduates);
        }

        private void ApplyFilters(ref IQueryable<Graduate> graduates, GetGraduatesQuery graduatesQuery)
        {
            graduates = BaseFindAll();

            /*
            if (graduatesQuery.MinCreateAt != null)
            {
                graduates = graduates.Where(x => x.CreateAt >= graduatesQuery.MinCreateAt);
            }

            if (graduatesQuery.MaxCreateAt != null)
            {
                graduates = graduates.Where(x => x.CreateAt < graduatesQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Graduate> graduates, string searchTerm)
        {
            if (!graduates.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            //graduates = graduates.Where(x => x.Name.ToLower().Contains(searchTerm.Trim().ToLower()));
        }


    }
}
