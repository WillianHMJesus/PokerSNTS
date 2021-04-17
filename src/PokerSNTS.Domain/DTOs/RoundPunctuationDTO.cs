using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RoundPointDTO
    {
        public RoundPointDTO(Guid id, short position, short Point, Guid playerId, Guid roundId)
        {
            Id = id;
            Position = position;
            Point = Point;
            PlayerId = playerId;
            RoundId = roundId;
        }

        public Guid Id { get; private set; }
        public short Position { get; private set; }
        public short Point { get; private set; }
        public Guid PlayerId { get; private set; }
        public Guid RoundId { get; private set; }
    }
}
