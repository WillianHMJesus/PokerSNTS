using FluentValidation;
using System;

namespace PokerSNTS.Domain.Entities
{
    public class PlayerRound : Entity
    {
        public PlayerRound(short position, short punctuation, Guid playerId, Guid roundId)
        {
            Position = position;
            Punctuation = punctuation;
            PlayerId = playerId;
            RoundId = roundId;
        }

        public short Position { get; private set; }
        public short Punctuation { get; private set; }
        public Guid PlayerId { get; private set; }
        public Guid RoundId { get; private set; }
        public virtual Player Player { get; private set; }
        public virtual Round Round { get; private set; }


        public override bool IsValid => Validate();

        private bool Validate()
        {
            var playerRoundaValidator = new PlayerRoundValidator();
            var validationResult = playerRoundaValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult.IsValid;
        }

        private class PlayerRoundValidator : AbstractValidator<PlayerRound>
        {
            public PlayerRoundValidator()
            {
                RuleFor(x => x.Position).NotNull().NotEqual(default(short)).WithMessage("A posição da rodada não foi informada.");
                RuleFor(x => x.Punctuation).NotNull().NotEqual(default(short)).WithMessage("A pontuação da rodada não foi informada.");
                RuleFor(x => x.PlayerId).NotNull().NotEqual(default(Guid)).WithMessage("O jogador não foi informado.");
                RuleFor(x => x.RoundId).NotNull().NotEqual(default(Guid)).WithMessage("A rodada não foi informado.");
            }
        }
    }
}
