using Application.Exceptions;
using Application.Features.Students.Queries.GetById;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Students.Commands.Update
{
    public class UpdateStudentCommand : IRequest<StudentViewModel>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
    }

    internal class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, StudentViewModel>
    {
        private readonly ILogger<UpdateStudentCommandHandler> _logger;
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdateStudentCommandHandler(IRepositoryWrapper repository, IMapper mapper, ILogger<UpdateStudentCommandHandler> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<StudentViewModel> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var studentEntity = await _repository.Student.GetByIdAsync(command.Id);
            if (studentEntity == null) throw new ApiException($"Student with id: {command.Id}, hasn't been found.");

            _mapper.Map(command, studentEntity);
            await _repository.Student.UpdateAsync(studentEntity);
            await _repository.SaveAsync();

            var studentReadDto = _mapper.Map<StudentViewModel>(studentEntity);
            //if (!string.IsNullOrWhiteSpace(studentReadDto.ImgLink)) studentReadDto.ImgLink = $"{_baseURL}{studentReadDto.ImgLink}";
            return studentReadDto;
        }
    }
}
