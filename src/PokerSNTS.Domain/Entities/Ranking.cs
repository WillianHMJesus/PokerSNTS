using FluentValidation;

namespace PokerSNTS.Domain.Entities
{
    public class Ranking : Entity
    {
        public Ranking(string description, decimal awardValue)
        {
            Description = description;
            AwardValue = awardValue;
        }

        public string Description { get; private set; }
        public decimal? AwardValue { get; private set; }

        public override bool IsValid => Validate();

        private bool Validate()
        {
            var rankingValidator = new RankingValidator();
            var validationResult = rankingValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult.IsValid;
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
