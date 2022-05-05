using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Queries.GetById
{
    public class GetStudentByIdQuery : IRequest<StudentViewModel>
    {
        public Guid Id { get; set; }
    }

    internal class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentViewModel>
    {
        private readonly ILogger<GetStudentByIdQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetStudentByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetStudentByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentViewModel> Handle(GetStudentByIdQuery query, CancellationToken cancellationToken)
        {
            var student = await _repository.Student.GetByIdAsync(query.Id);
            if (student == null) throw new ApiException($"Student with id: {query.Id}, hasn't been found.");

            _logger.LogInformation($"Returned Student with id: {query.Id}");
            return _mapper.Map<StudentViewModel>(student);
        }
    }
}
