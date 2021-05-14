using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingPointDTO
    {
        public RankingPointDTO(Guid id, short position, short point)
        {
            Id = id;
            Position = position;
            Point = point;
        }

        public Guid Id { get; private set; }
        public short Position { get; private set; }
        public short Point { get; private set; }
    }
}
