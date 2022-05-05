using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Dimplomas.Commands.Create
{
    public class CreateDimplomaCommandValidator : AbstractValidator<CreateDimplomaCommand>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public CreateDimplomaCommandValidator(IRepositoryWrapper repository, IMapper mapper)
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

        private async Task<bool> IsUnique(CreateDimplomaCommand dimplomaCommand, CancellationToken cancellationToken)
        {
            var dimploma = _mapper.Map<Dimploma>(dimplomaCommand);
            return !(await _repository.Dimploma.ExistAsync(dimploma));
        }
    }
}
