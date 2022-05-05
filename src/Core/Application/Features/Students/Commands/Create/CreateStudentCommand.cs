using Application.Features.Students.Queries.GetById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Commands.Create
{
    public class CreateStudentCommand : IRequest<StudentViewModel>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }

    internal class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, StudentViewModel>
    {
        private readonly ILogger<CreateStudentCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateStudentCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<CreateStudentCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<StudentViewModel> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var student = _mapper.Map<Student>(command);

            await _repository.Student.CreateAsync(student);
            await _repository.SaveAsync();

            return _mapper.Map<StudentViewModel>(student);
        }
    }
}
