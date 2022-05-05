using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Commands.Delete
{
    public class DeleteStudentCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly ILogger<DeleteStudentCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;

        public DeleteStudentCommandHandler(IRepositoryWrapper repository, ILogger<DeleteStudentCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteStudentCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.Student.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException($"Student with id: {command.Id}, hasn't been found.");
            await _repository.Student.DeleteAsync(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
