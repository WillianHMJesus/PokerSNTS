using FluentValidation.Results;
using System;

namespace PokerSNTS.Domain.Entities
{
    public abstract class Entity
    {
        public Entity(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public ValidationResult ValidationResult { get; private set; }
    }
}
