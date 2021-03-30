using FluentValidation.Results;
using System;

namespace PokerSNTS.Domain.Entities
{
    public abstract class Entity
    {
        public Entity() { Id = Guid.NewGuid(); }

        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public bool Actived { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract ValidationResult Validate();

        public void SetValidationResult(ValidationResult validationResult)
        {
            if (ValidationResult == null)
            {
                ValidationResult = validationResult;
                return;
            }
            
            foreach (var validationFailure in validationResult.Errors)
            {
                ValidationResult.Errors.Add(validationFailure);
            }
        }
    }
}
