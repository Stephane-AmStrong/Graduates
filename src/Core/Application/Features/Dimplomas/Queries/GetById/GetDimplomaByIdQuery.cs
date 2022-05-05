using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Queries.GetById
{
    public class GetDimplomaByIdQuery : IRequest<DimplomaViewModel>
    {
        public Guid Id { get; set; }
    }

    internal class GetDimplomaByIdQueryHandler : IRequestHandler<GetDimplomaByIdQuery, DimplomaViewModel>
    {
        private readonly ILogger<GetDimplomaByIdQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetDimplomaByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetDimplomaByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DimplomaViewModel> Handle(GetDimplomaByIdQuery query, CancellationToken cancellationToken)
        {
            var dimploma = await _repository.Dimploma.GetByIdAsync(query.Id);
            if (dimploma == null) throw new ApiException($"Dimploma with id: {query.Id}, hasn't been found.");

            _logger.LogInformation($"Returned Dimploma with id: {query.Id}");
            return _mapper.Map<DimplomaViewModel>(dimploma);
        }
    }
}
