﻿using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingDTO
    {
        public RankingDTO(Ranking ranking)
        {
            Id = ranking.Id;
            Description = ranking.Description;
            AwardValue = ranking.AwardValue;
        }

        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public decimal? AwardValue { get; private set; }
    }
}
