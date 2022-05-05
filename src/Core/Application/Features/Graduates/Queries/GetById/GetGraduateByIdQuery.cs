using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Graduates.Queries.GetById
{
    public class GetGraduateByIdQuery : IRequest<GraduateViewModel>
    {
        public Guid Id { get; set; }
    }

    internal class GetGraduateByIdQueryHandler : IRequestHandler<GetGraduateByIdQuery, GraduateViewModel>
    {
        private readonly ILogger<GetGraduateByIdQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetGraduateByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetGraduateByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GraduateViewModel> Handle(GetGraduateByIdQuery query, CancellationToken cancellationToken)
        {
            var graduate = await _repository.Graduate.GetByIdAsync(query.Id);
            if (graduate == null) throw new ApiException($"Graduate with id: {query.Id}, hasn't been found.");

            _logger.LogInformation($"Returned Graduate with id: {query.Id}");
            return _mapper.Map<GraduateViewModel>(graduate);
        }
    }
}
