using System;
using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RoundInputModel
    {
        [Required(ErrorMessage = "A descrição da rodada não foi informada.")]
        [MaxLength(50, ErrorMessage = "A descrição da rodada permite o número máximo de 100 caracters.")]
        public string Description { get; set; }

        [RegularExpression("^((?!01/01/0001 00:00:00).)*$", ErrorMessage = "A data da rodada não foi informada.")]
        public DateTime Date { get; set; }

        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "O ranking não foi informado.")]
        public Guid RankingId { get; set; }
    }
}
