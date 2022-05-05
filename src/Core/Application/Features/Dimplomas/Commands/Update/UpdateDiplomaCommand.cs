using Application.Exceptions;
using Application.Features.Diplomas.Queries.GetById;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Commands.Update
{
    public class UpdateDiplomaCommand : IRequest<DiplomaViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class UpdateDiplomaCommandHandler : IRequestHandler<UpdateDiplomaCommand, DiplomaViewModel>
    {
        private readonly ILogger<UpdateDiplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdateDiplomaCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<UpdateDiplomaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DiplomaViewModel> Handle(UpdateDiplomaCommand command, CancellationToken cancellationToken)
        {
            var diplomaEntity = await _repository.Diploma.GetByIdAsync(command.Id);
            if (diplomaEntity == null) throw new ApiException($"Diploma with id: {command.Id}, hasn't been found.");

            _mapper.Map(command, diplomaEntity);
            await _repository.Diploma.UpdateAsync(diplomaEntity);
            await _repository.SaveAsync();

            var diplomaReadDto = _mapper.Map<DiplomaViewModel>(diplomaEntity);
            //if (!string.IsNullOrWhiteSpace(diplomaReadDto.ImgLink)) diplomaReadDto.ImgLink = $"{_baseURL}{diplomaReadDto.ImgLink}";
            return diplomaReadDto;
        }
    }
}
