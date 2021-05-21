using System;
using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RoundPointInputModel
    {
        [Range(1, 9999999999999, ErrorMessage = "A posição da rodada não foi informada.")]
        public short Position { get; set; }

        [Range(1, 9999999999999, ErrorMessage = "A pontuação da rodada não foi informada.")]
        public short Point { get; set; }

        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "O jogador não foi informado.")]
        public Guid PlayerId { get; set; }

        [RegularExpression("^((?!00000000-0000-0000-0000-000000000000).)*$", ErrorMessage = "A rodada não foi informado.")]
        public Guid RoundId { get; set; }
    }
}
