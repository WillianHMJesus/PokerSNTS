using System.ComponentModel.DataAnnotations;

namespace PokerSNTS.API.InputModels
{
    public class RegulationInputModel
    {
        [Required(ErrorMessage = "A descrição do regulamento não foi informada.")]
        public string Description { get; set; }
    }
}
