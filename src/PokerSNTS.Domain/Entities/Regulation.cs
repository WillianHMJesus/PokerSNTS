using FluentValidation;
using FluentValidation.Results;

namespace PokerSNTS.Domain.Entities
{
    public class Regulation : Entity
    {
        public Regulation(string description)
        {
            Description = description;
        }

        protected Regulation() { }

        public string Description { get; private set; }

        public override ValidationResult Validate()
        {
            var regulationValidator = new RegulationValidator();
            var validationResult = regulationValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(string description)
        {
            Description = description;
        }

        private class RegulationValidator : AbstractValidator<Regulation>
        {
            public RegulationValidator()
            {
                RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("A descrição do regulamento não foi informada.");
            }
        }
    }
}
