using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RegulationDTO
    {
        public RegulationDTO(Guid id, string description)
        {
            Id = id;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
    }
}
