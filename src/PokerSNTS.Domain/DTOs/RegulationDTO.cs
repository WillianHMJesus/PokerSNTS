using PokerSNTS.Domain.Entities;
using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RegulationDTO
    {
        public RegulationDTO(Regulation regulation)
        {
            Id = regulation.Id;
            Description = regulation.Description;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
    }
}
