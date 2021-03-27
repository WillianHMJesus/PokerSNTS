using FluentValidation;

namespace PokerSNTS.Domain.Entities
{
    public class Regulation : Entity
    {
        public Regulation(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }

        public override bool IsValid => Validate();

        private bool Validate()
        {
            var regulationValidator = new RegulationValidator();
            var validationResult = regulationValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult.IsValid;
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
