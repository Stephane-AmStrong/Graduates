using Application.Features.Students.Queries.GetPagedList;
using Application.Wrappers;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<PagedList<Student>> GetPagedListAsync(GetStudentsQuery getStudentsQuery);

        Task<Student> GetByIdAsync(Guid id);
        Task<bool> ExistAsync(Student student);

        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(Student student);
        Task UpdateAsync(IEnumerable<Student> students);
        Task DeleteAsync(IEnumerable<Student> students);
    }
}
