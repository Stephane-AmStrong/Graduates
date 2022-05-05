using Application.Features.Students.Queries.GetPagedList;
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
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        private ISortHelper<Student> _sortHelper;

        public StudentRepository
        (
            ApplicationDbContext appDbContext,
            ISortHelper<Student> sortHelper
        ) : base(appDbContext)
        {
            _sortHelper = sortHelper;
        }

        public async Task<PagedList<Student>> GetPagedListAsync(GetStudentsQuery studentsQuery)
        {
            var students = Enumerable.Empty<Student>().AsQueryable();

            ApplyFilters(ref students, studentsQuery);

            PerformSearch(ref students, studentsQuery.SearchTerm);

            var sortedStudents = _sortHelper.ApplySort(students, studentsQuery.OrderBy);

            return await Task.Run(() =>
                PagedList<Student>.ToPagedList
                (
                    sortedStudents,
                    studentsQuery.PageNumber,
                    studentsQuery.PageSize)
                );
        }


        public async Task<Student> GetByIdAsync(Guid id)
        {
            return await BaseFindByCondition(student => student.Id.Equals(id))
                .Include(x => x.Graduates)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAsync(Student student)
        {
            return await BaseFindByCondition(x => x.FirstName == student.FirstName && x.LastName == student.LastName)
                .AnyAsync();
        }

        public async Task CreateAsync(Student student)
        {
            await BaseCreateAsync(student);
        }

        public async Task UpdateAsync(Student student)
        {
            await BaseUpdateAsync(student);
        }

        public async Task DeleteAsync(Student student)
        {
            await BaseDeleteAsync(student);
        }

        public async Task UpdateAsync(IEnumerable<Student> students)
        {
            await BaseUpdateAsync(students);
        }

        public async Task DeleteAsync(IEnumerable<Student> students)
        {
            await BaseDeleteAsync(students);
        }

        private void ApplyFilters(ref IQueryable<Student> students, GetStudentsQuery studentsQuery)
        {
            students = BaseFindAll();

            /*
            if (studentsQuery.MinCreateAt != null)
            {
                students = students.Where(x => x.CreateAt >= studentsQuery.MinCreateAt);
            }

            if (studentsQuery.MaxCreateAt != null)
            {
                students = students.Where(x => x.CreateAt < studentsQuery.MaxCreateAt);
            }
            */
        }

        private void PerformSearch(ref IQueryable<Student> students, string searchTerm)
        {
            if (!students.Any() || string.IsNullOrWhiteSpace(searchTerm)) return;

            students = students.Where(x => x.FirstName.ToLower().Contains(searchTerm.Trim().ToLower()) || x.LastName.ToLower().Contains(searchTerm.Trim().ToLower()));
        }


    }
}
