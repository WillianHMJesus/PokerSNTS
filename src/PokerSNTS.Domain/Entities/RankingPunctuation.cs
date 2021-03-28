using FluentValidation;
using FluentValidation.Results;

namespace PokerSNTS.Domain.Entities
{
    public class RankingPunctuation : Entity
    {
        public RankingPunctuation(short position, short punctuation)
        {
            Position = position;
            Punctuation = punctuation;
        }

        protected RankingPunctuation() { }

        public short Position { get; private set; }
        public short Punctuation { get; private set; }

        public override ValidationResult Validate()
        {
            var rankingPunctuationValidator = new RankingPunctuationValidator();
            var validationResult = rankingPunctuationValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(short position, short punctuation)
        {
            Position = position;
            Punctuation = punctuation;
        }

        private class RankingPunctuationValidator : AbstractValidator<RankingPunctuation>
        {
            public RankingPunctuationValidator()
            {
                RuleFor(x => x.Position).NotNull().NotEqual(default(short)).WithMessage("A posição do ranking não foi informada.");
                RuleFor(x => x.Punctuation).NotNull().NotEqual(default(short)).WithMessage("A pontuação do ranking não foi informada.");
            }
        }
    }
}
