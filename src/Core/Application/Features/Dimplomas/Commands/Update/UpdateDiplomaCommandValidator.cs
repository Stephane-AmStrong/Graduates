using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Commands.Update
{
    public class UpdateDiplomaCommandValidator : AbstractValidator<UpdateDiplomaCommand>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdateDiplomaCommandValidator(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeAValidGuid).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(60).WithMessage("{PropertyName} must not exceed 60 characters.");
        }

        private bool BeAValidGuid(Guid id)
        {
            return !id.Equals(new Guid());
        }
    }
}
