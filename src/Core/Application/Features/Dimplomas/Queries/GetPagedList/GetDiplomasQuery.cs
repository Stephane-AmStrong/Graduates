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

namespace Application.Features.Diplomas.Queries.GetPagedList
{
    public class GetDiplomasQuery : QueryParameters, IRequest<PagedListResponse<DiplomasViewModel>>
    {
        public GetDiplomasQuery()
        {
            OrderBy = "name";
        }

        public string? WithTheName { get; set; }
    }

    internal class GetDiplomasQueryHandler : IRequestHandler<GetDiplomasQuery, PagedListResponse<DiplomasViewModel>>
    {
        private readonly ILogger<GetDiplomasQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetDiplomasQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetDiplomasQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedListResponse<DiplomasViewModel>> Handle(GetDiplomasQuery query, CancellationToken cancellationToken)
        {
            var diplomas = await _repository.Diploma.GetPagedListAsync(query);
            var diplomasViewModel = _mapper.Map<List<DiplomasViewModel>>(diplomas);
            _logger.LogInformation($"Returned Paged List of Diplomas from database.");
            return new PagedListResponse<DiplomasViewModel>(diplomasViewModel, diplomas.MetaData);
        }
    }
}
