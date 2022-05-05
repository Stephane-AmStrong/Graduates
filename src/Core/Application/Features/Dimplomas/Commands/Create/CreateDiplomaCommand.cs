using Application.Features.Diplomas.Queries.GetById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Commands.Create
{
    public class CreateDiplomaCommand : IRequest<DiplomaViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class CreateDiplomaCommandHandler : IRequestHandler<CreateDiplomaCommand, DiplomaViewModel>
    {
        private readonly ILogger<CreateDiplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateDiplomaCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<CreateDiplomaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<DiplomaViewModel> Handle(CreateDiplomaCommand command, CancellationToken cancellationToken)
        {
            var diploma = _mapper.Map<Diploma>(command);

            await _repository.Diploma.CreateAsync(diploma);
            await _repository.SaveAsync();

            return _mapper.Map<DiplomaViewModel>(diploma);
        }
    }
}
