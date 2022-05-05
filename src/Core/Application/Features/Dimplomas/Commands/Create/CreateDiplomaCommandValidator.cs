using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Diplomas.Commands.Create
{
    public class CreateDiplomaCommandValidator : AbstractValidator<CreateDiplomaCommand>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CreateDiplomaCommandValidator(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p)
                .MustAsync(IsUnique).WithMessage("{PropertyName} already exists.");
        }

        private async Task<bool> IsUnique(CreateDiplomaCommand diplomaCommand, CancellationToken cancellationToken)
        {
            var diploma = _mapper.Map<Diploma>(diplomaCommand);
            return !(await _repository.Diploma.ExistAsync(diploma));
        }
    }
}
