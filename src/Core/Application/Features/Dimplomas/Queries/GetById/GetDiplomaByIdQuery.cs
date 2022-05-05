using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Queries.GetById
{
    public class GetDiplomaByIdQuery : IRequest<DiplomaViewModel>
    {
        public Guid Id { get; set; }
    }

    internal class GetDiplomaByIdQueryHandler : IRequestHandler<GetDiplomaByIdQuery, DiplomaViewModel>
    {
        private readonly ILogger<GetDiplomaByIdQueryHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetDiplomaByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<GetDiplomaByIdQueryHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DiplomaViewModel> Handle(GetDiplomaByIdQuery query, CancellationToken cancellationToken)
        {
            var diploma = await _repository.Diploma.GetByIdAsync(query.Id);
            if (diploma == null) throw new ApiException($"Diploma with id: {query.Id}, hasn't been found.");

            _logger.LogInformation($"Returned Diploma with id: {query.Id}");
            return _mapper.Map<DiplomaViewModel>(diploma);
        }
    }
}
