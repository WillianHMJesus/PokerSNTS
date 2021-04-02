using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RegulationAdapter
    {
        public static RegulationDTO ToRegulationDTO(Regulation regulation)
        {
            return new RegulationDTO(regulation.Id, regulation.Description);
        }
    }
}
