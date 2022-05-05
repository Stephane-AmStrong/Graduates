using Application.Enums;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Parameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Queries.GetPagedList
{
    public class GetStudentsQuery : QueryParameters, IRequest<PagedListResponse<StudentsViewModel>>
    {
        public GetStudentsQuery()
        {
            OrderBy = "LastName, FirstName";
        }

        public string? WithTheName { get; set; }
    }

    internal class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, PagedListResponse<StudentsViewModel>>
    {
        private readonly ILogger<GetStudentsQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetStudentsQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetStudentsQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedListResponse<StudentsViewModel>> Handle(GetStudentsQuery query, CancellationToken cancellationToken)
        {
            var students = await _repository.Student.GetPagedListAsync(query);
            var studentsViewModel = _mapper.Map<List<StudentsViewModel>>(students);
            _logger.LogInformation($"Returned Paged List of Students from database.");
            return new PagedListResponse<StudentsViewModel>(studentsViewModel, students.MetaData);
        }
    }
}
