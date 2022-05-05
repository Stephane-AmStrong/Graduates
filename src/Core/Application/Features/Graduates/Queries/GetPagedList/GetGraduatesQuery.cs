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

namespace Application.Features.Graduates.Queries.GetPagedList
{
    public class GetGraduatesQuery : QueryParameters, IRequest<PagedListResponse<GraduatesViewModel>>
    {
        public GetGraduatesQuery()
        {
            OrderBy = "GraduateAt desc";
        }

        public string? WithTheName { get; set; }
    }

    internal class GetGraduatesQueryHandler : IRequestHandler<GetGraduatesQuery, PagedListResponse<GraduatesViewModel>>
    {
        private readonly ILogger<GetGraduatesQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetGraduatesQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetGraduatesQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedListResponse<GraduatesViewModel>> Handle(GetGraduatesQuery query, CancellationToken cancellationToken)
        {
            var graduates = await _repository.Graduate.GetPagedListAsync(query);
            var graduatesViewModel = _mapper.Map<List<GraduatesViewModel>>(graduates);
            _logger.LogInformation($"Returned Paged List of Graduates from database.");
            return new PagedListResponse<GraduatesViewModel>(graduatesViewModel, graduates.MetaData);
        }
    }
}
