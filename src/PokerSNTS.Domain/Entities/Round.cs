using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace PokerSNTS.Domain.Entities
{
    public class Round : Entity
    {
        public Round(string description, DateTime date, Guid rankingId)
        {
            Description = description;
            Date = date;
            RankingId = rankingId;
        }

        protected Round() { }

        public string Description { get; private set; }
        public DateTime Date { get; private set; }
        public Guid RankingId { get; private set; }
        public virtual Ranking Ranking { get; private set; }
        public virtual ICollection<RoundPunctuation> RoundsPunctuations { get; private set; }

        public override ValidationResult Validate()
        {
            var roundValidator = new RoundValidator();
            var validationResult = roundValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(string description, DateTime date, Guid rankingId)
        {
            Description = description;
            Date = date;
            RankingId = rankingId;
        }

        private class RoundValidator : AbstractValidator<Round>
        {
            public RoundValidator()
            {
                RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("A descrição da rodada não foi informada.");
                RuleFor(x => x.Description).MaximumLength(100).WithMessage("A descrição da rodada permite o número máximo de 100 caracters.");
                RuleFor(x => x.Date).NotNull().NotEqual(default(DateTime)).WithMessage("A data da rodada não foi informada.");
                RuleFor(x => x.RankingId).NotNull().NotEqual(default(Guid)).WithMessage("O ranking não foi informado.");
            }
        }
    }
}
