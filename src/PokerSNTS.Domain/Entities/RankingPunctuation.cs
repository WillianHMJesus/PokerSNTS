using FluentValidation;
using FluentValidation.Results;

namespace PokerSNTS.Domain.Entities
{
    public class RankingPoint : Entity
    {
        public RankingPoint(short position, short Point)
        {
            Position = position;
            Point = Point;
        }

        protected RankingPoint() { }

        public short Position { get; private set; }
        public short Point { get; private set; }

        public override ValidationResult Validate()
        {
            var rankingPointValidator = new RankingPointValidator();
            var validationResult = rankingPointValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(short position, short Point)
        {
            Position = position;
            Point = Point;
        }

        private class RankingPointValidator : AbstractValidator<RankingPoint>
        {
            public RankingPointValidator()
            {
                RuleFor(x => x.Position).NotNull().NotEqual(default(short)).WithMessage("A posição do ranking não foi informada.");
                RuleFor(x => x.Point).NotNull().NotEqual(default(short)).WithMessage("A pontuação do ranking não foi informada.");
            }
        }
    }
}
