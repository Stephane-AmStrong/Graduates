using Application.Exceptions;
using Application.Features.Dimplomas.Queries.GetById;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Commands.Update
{
    public class UpdateDimplomaCommand : IRequest<DimplomaViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    internal class UpdateDimplomaCommandHandler : IRequestHandler<UpdateDimplomaCommand, DimplomaViewModel>
    {
        private readonly ILogger<UpdateDimplomaCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdateDimplomaCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<UpdateDimplomaCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<DimplomaViewModel> Handle(UpdateDimplomaCommand command, CancellationToken cancellationToken)
        {
            var dimplomaEntity = await _repository.Dimploma.GetByIdAsync(command.Id);
            if (dimplomaEntity == null) throw new ApiException($"Dimploma with id: {command.Id}, hasn't been found.");

            _mapper.Map(command, dimplomaEntity);
            await _repository.Dimploma.UpdateAsync(dimplomaEntity);
            await _repository.SaveAsync();

            var dimplomaReadDto = _mapper.Map<DimplomaViewModel>(dimplomaEntity);
            //if (!string.IsNullOrWhiteSpace(dimplomaReadDto.ImgLink)) dimplomaReadDto.ImgLink = $"{_baseURL}{dimplomaReadDto.ImgLink}";
            return dimplomaReadDto;
        }
    }
}
