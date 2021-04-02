using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class PlayerInputModel
    {
        [Required(ErrorMessage = "O nome do jogador não foi informado.")]
        [MaxLength(50, ErrorMessage = "O nome do jogador permite o número máximo de 50 caracters.")]
        public string Name { get; set; }
    }
}
