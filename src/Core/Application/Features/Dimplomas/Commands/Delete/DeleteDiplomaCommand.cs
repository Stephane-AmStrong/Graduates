using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Commands.Delete
{
    public class DeleteDiplomaCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteDiplomaCommandHandler : IRequestHandler<DeleteDiplomaCommand>
    {
        private readonly ILogger<DeleteDiplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;

        public DeleteDiplomaCommandHandler(IRepositoryWrapper repository, ILogger<DeleteDiplomaCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDiplomaCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.Diploma.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException($"Diploma with id: {command.Id}, hasn't been found.");
            await _repository.Diploma.DeleteAsync(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
