using FluentValidation;
using FluentValidation.Results;
using System;

namespace PokerSNTS.Domain.Entities
{
    public class RoundPoint : Entity
    {
        public RoundPoint(short position, short Point, Guid playerId, Guid roundId)
        {
            Position = position;
            Point = Point;
            PlayerId = playerId;
            RoundId = roundId;
        }

        protected RoundPoint() { }

        public short Position { get; private set; }
        public short Point { get; private set; }
        public Guid PlayerId { get; private set; }
        public Guid RoundId { get; private set; }
        public virtual Player Player { get; private set; }
        public virtual Round Round { get; private set; }

        public override ValidationResult Validate()
        {
            var roundPointValidator = new RoundPointValidator();
            var validationResult = roundPointValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(short position, short Point, Guid playerId, Guid roundId)
        {
            Position = position;
            Point = Point;
            PlayerId = playerId;
            RoundId = roundId;
        }

        private class RoundPointValidator : AbstractValidator<RoundPoint>
        {
            public RoundPointValidator()
            {
                RuleFor(x => x.Position).NotNull().NotEqual(default(short)).WithMessage("A posição da rodada não foi informada.");
                RuleFor(x => x.Point).NotNull().NotEqual(default(short)).WithMessage("A pontuação da rodada não foi informada.");
                RuleFor(x => x.PlayerId).NotNull().NotEqual(default(Guid)).WithMessage("O jogador não foi informado.");
                RuleFor(x => x.RoundId).NotNull().NotEqual(default(Guid)).WithMessage("A rodada não foi informado.");
            }
        }
    }
}
