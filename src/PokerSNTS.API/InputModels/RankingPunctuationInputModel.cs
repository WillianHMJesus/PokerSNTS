using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RankingPunctuationInputModel
    {
        [RegularExpression("^((?!0).)*$", ErrorMessage = "A posição da rodada não foi informada.")]
        public short Position { get; set; }

        [RegularExpression("^((?!0).)*$", ErrorMessage = "A pontuação da rodada não foi informada.")]
        public short Punctuation { get; set; }
    }
}
