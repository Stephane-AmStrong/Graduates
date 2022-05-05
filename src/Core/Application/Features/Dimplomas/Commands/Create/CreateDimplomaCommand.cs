using Application.Features.Dimplomas.Queries.GetById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Commands.Create
{
    public class CreateDimplomaCommand : IRequest<DimplomaViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class CreateDimplomaCommandHandler : IRequestHandler<CreateDimplomaCommand, DimplomaViewModel>
    {
        private readonly ILogger<CreateDimplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateDimplomaCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<CreateDimplomaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<DimplomaViewModel> Handle(CreateDimplomaCommand command, CancellationToken cancellationToken)
        {
            var dimploma = _mapper.Map<Dimploma>(command);

            await _repository.Dimploma.CreateAsync(dimploma);
            await _repository.SaveAsync();

            return _mapper.Map<DimplomaViewModel>(dimploma);
        }
    }
}
