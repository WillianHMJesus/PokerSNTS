using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RankingInputModel
    {
        [Required(ErrorMessage = "A descrição do ranking não foi informada.")]
        [MaxLength(50, ErrorMessage = "A descrição do ranking permite o número máximo de 100 caracters.")]
        public string Description { get; set; }

        public decimal? AwardValue { get; set; }
    }
}
