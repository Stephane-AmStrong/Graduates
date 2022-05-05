using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Commands.Delete
{
    public class DeleteDimplomaCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteDimplomaCommandHandler : IRequestHandler<DeleteDimplomaCommand>
    {
        private readonly ILogger<DeleteDimplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;

        public DeleteDimplomaCommandHandler(IRepositoryWrapper repository, ILogger<DeleteDimplomaCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDimplomaCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.Dimploma.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException($"Dimploma with id: {command.Id}, hasn't been found.");
            await _repository.Dimploma.DeleteAsync(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
