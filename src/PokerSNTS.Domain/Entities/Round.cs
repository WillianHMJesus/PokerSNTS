using FluentValidation;
using System;

namespace PokerSNTS.Domain.Entities
{
    public class Round : Entity
    {
        public Round(string description, DateTime date)
        {
            Description = description;
            Date = date;
        }

        public string Description { get; private set; }
        public DateTime Date { get; private set; }

        public override bool IsValid => Validate();

        private bool Validate()
        {
            var roundValidator = new RoundValidator();
            var validationResult = roundValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult.IsValid;
        }

        private class RoundValidator : AbstractValidator<Round>
        {
            public RoundValidator()
            {
                RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("A descrição da rodada não foi informada.");
                RuleFor(x => x.Description).MaximumLength(100).WithMessage("A descrição da rodada permite o número máximo de 100 caracters.");
                RuleFor(x => x.Date).NotNull().NotEqual(default(DateTime)).WithMessage("A data da rodada não foi informada.");
            }
        }
    }
}
