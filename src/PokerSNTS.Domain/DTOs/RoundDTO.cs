using PokerSNTS.Domain.Entities;
using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RoundDTO
    {
        public RoundDTO(Round round)
        {
            Id = round.Id;
            Description = round.Description;
            Date = round.Date;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public DateTime Date { get; private set; }
    }
}
