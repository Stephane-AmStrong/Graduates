using Application.Exceptions;
using Application.Features.Graduates.Queries.GetById;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Graduates.Commands.Update
{
    public class UpdateGraduateCommand : IRequest<GraduateViewModel>
    {
        public Guid Id { get; set; }
        public DateTime GraduateAt { get; set; }
        public Guid DiplomaId { get; set; }
        public Guid StudentId { get; set; }
    }

    internal class UpdateGraduateCommandHandler : IRequestHandler<UpdateGraduateCommand, GraduateViewModel>
    {
        private readonly ILogger<UpdateGraduateCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdateGraduateCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<UpdateGraduateCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GraduateViewModel> Handle(UpdateGraduateCommand command, CancellationToken cancellationToken)
        {
            var graduateEntity = await _repository.Graduate.GetByIdAsync(command.Id);
            if (graduateEntity == null) throw new ApiException($"Graduate with id: {command.Id}, hasn't been found.");

            _mapper.Map(command, graduateEntity);
            await _repository.Graduate.UpdateAsync(graduateEntity);
            await _repository.SaveAsync();

            var graduateReadDto = _mapper.Map<GraduateViewModel>(graduateEntity);
            //if (!string.IsNullOrWhiteSpace(graduateReadDto.ImgLink)) graduateReadDto.ImgLink = $"{_baseURL}{graduateReadDto.ImgLink}";
            return graduateReadDto;
        }
    }
}
