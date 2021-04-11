using FluentValidation;
using FluentValidation.Results;

namespace PokerSNTS.Domain.Entities
{
    public class User : Entity
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }

        public override ValidationResult Validate()
        {
            var userValidator = new UserValidator();
            var validationResult = userValidator.Validate(this);
            SetValidationResult(validationResult);

            return validationResult;
        }

        public void Update(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        private class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(x => x.UserName).NotNull().NotEmpty().WithMessage("O nome do usuário não foi informado.");
                RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("A senha do usuário não foi informada");
                RuleFor(x => x.Password).MinimumLength(6).WithMessage("A senha do usuário permite o número mínimo de 6 caracters.");
            }
        }
    }
}
