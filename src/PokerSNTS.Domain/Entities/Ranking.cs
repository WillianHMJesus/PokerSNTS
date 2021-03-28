using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Entities
{
    public class Ranking : Entity
    {
        public Ranking(string description, decimal? awardValue)
        {
            Description = description;
            AwardValue = awardValue;
        }

        protected Ranking() { }

        public string Description { get; private set; }
        public decimal? AwardValue { get; private set; }
        public virtual ICollection<Round> Rounds { get; private set; }

        public override ValidationResult Validate()
        {
            var rankingValidator = new RankingValidator();
            var validationResult = rankingValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(string description, decimal? awardValue)
        {
            Description = description;
            AwardValue = awardValue;
        }

        private class RankingValidator : AbstractValidator<Ranking>
        {
            public RankingValidator()
            {
                RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("A descrição do ranking não foi informada.");
                RuleFor(x => x.Description).MaximumLength(100).WithMessage("A descrição do ranking permite o número máximo de 100 caracters.");
            }
        }
    }
}
