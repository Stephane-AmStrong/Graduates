using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Graduates.Commands.Delete
{
    public class DeleteGraduateCommand : IRequest
    {
        public Guid Id { get; set; }
    }

    internal class DeleteGraduateCommandHandler : IRequestHandler<DeleteGraduateCommand>
    {
        private readonly ILogger<DeleteGraduateCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;

        public DeleteGraduateCommandHandler(IRepositoryWrapper repository, ILogger<DeleteGraduateCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteGraduateCommand command, CancellationToken cancellationToken)
        {
            var product = await _repository.Graduate.GetByIdAsync(command.Id);
            if (product == null) throw new ApiException($"Graduate with id: {command.Id}, hasn't been found.");
            await _repository.Graduate.DeleteAsync(product);
            await _repository.SaveAsync();

            return Unit.Value;
        }
    }
}
