using Application.Enums;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Parameters;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Queries.GetPagedList
{
    public class GetDimplomasQuery : QueryParameters, IRequest<PagedListResponse<DimplomasViewModel>>
    {
        public GetDimplomasQuery()
        {
            OrderBy = "name";
        }

        public string? WithTheName { get; set; }
    }

    internal class GetDimplomasQueryHandler : IRequestHandler<GetDimplomasQuery, PagedListResponse<DimplomasViewModel>>
    {
        private readonly ILogger<GetDimplomasQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetDimplomasQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetDimplomasQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedListResponse<DimplomasViewModel>> Handle(GetDimplomasQuery query, CancellationToken cancellationToken)
        {
            var dimplomas = await _repository.Dimploma.GetPagedListAsync(query);
            var dimplomasViewModel = _mapper.Map<List<DimplomasViewModel>>(dimplomas);
            _logger.LogInformation($"Returned Paged List of Dimplomas from database.");
            return new PagedListResponse<DimplomasViewModel>(dimplomasViewModel, dimplomas.MetaData);
        }
    }
}
