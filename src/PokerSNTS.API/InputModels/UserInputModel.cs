using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class UserInputModel
    {
        [Required(ErrorMessage = "O nome do usuário não foi informado")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha do usuário não foi informada")]
        [MinLength(6, ErrorMessage = "A senha do usuário permite o número mínimo de 6 caracters.")]
        public string Password { get; set; }
    }
}
