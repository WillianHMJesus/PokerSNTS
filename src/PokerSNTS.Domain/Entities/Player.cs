using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Entities
{
    public class Player : Entity
    {
        public Player(string name)
        {
            Name = name;
        }

        protected Player() { }

        public string Name { get; private set; }
        public virtual ICollection<RoundPunctuation> RoundsPunctuations { get; private set; }

        public override ValidationResult Validate()
        {
            var playerValidator = new PlayerValidator();
            var validationResult = playerValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(string name)
        {
            Name = name;
        }

        private class PlayerValidator : AbstractValidator<Player>
        {
            public PlayerValidator()
            {
                RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("O nome do jogador não foi informado.");
                RuleFor(x => x.Name).MaximumLength(50).WithMessage("O nome do jogador permite o número máximo de 50 caracters.");
            }
        }
    }
}
