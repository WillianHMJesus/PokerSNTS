using FluentValidation.Results;
using System;

namespace PokerSNTS.Domain.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public ValidationResult ValidationResult { get; private set; }
        public abstract bool IsValid { get; }

        public void SetValidationResult(ValidationResult validationResult) => ValidationResult = validationResult;
    }
}
