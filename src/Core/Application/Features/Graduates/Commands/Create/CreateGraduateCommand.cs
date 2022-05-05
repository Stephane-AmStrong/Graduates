using Application.Features.Graduates.Queries.GetById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Graduates.Commands.Create
{
    public class CreateGraduateCommand : IRequest<GraduateViewModel>
    {
        public DateTime GraduateAt { get; set; }
        public Guid DimplomaId { get; set; }
        public Guid StudentId { get; set; }

    }

    internal class CreateGraduateCommandHandler : IRequestHandler<CreateGraduateCommand, GraduateViewModel>
    {
        private readonly ILogger<CreateGraduateCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateGraduateCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<CreateGraduateCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task<GraduateViewModel> Handle(CreateGraduateCommand command, CancellationToken cancellationToken)
        {
            var graduate = _mapper.Map<Graduate>(command);

            await _repository.Graduate.CreateAsync(graduate);
            await _repository.SaveAsync();

            return _mapper.Map<GraduateViewModel>(graduate);
        }
    }
}
