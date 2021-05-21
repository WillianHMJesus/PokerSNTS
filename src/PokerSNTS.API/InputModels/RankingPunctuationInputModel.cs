using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RankingPointInputModel
    {
        [Range(1, 9999999999999, ErrorMessage = "A posição da rodada não foi informada.")]
        public short Position { get; set; }

        [Range(1, 9999999999999, ErrorMessage = "A pontuação da rodada não foi informada.")]
        public short Point { get; set; }
    }
}
