using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RankingInputModel
    {
        [Required(ErrorMessage = "A descrição do ranking não foi informada.")]
        public string Description { get; set; }

        public decimal? AwardValue { get; set; }
    }
}
