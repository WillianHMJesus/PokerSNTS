using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.Adapters;
using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.API.Controllers
{
    public class RoundController : BaseController
    {
        private readonly IRoundService _roundService;
        private readonly IRoundPunctuationService _roundPunctuationService;

        public RoundController(IRoundService roundService,
            IRoundPunctuationService roundPunctuationService,
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _roundService = roundService;
            _roundPunctuationService = roundPunctuationService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RoundInputModel model)
        {
            if (ModelState.IsValid)
            {
                var round = new Round(model.Description, model.Date, model.RankingId);
                var result = await _roundService.AddAsync(round);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RoundInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id da rodada não foi informado.");

                return Response();
            }

            if (ModelState.IsValid)
            {
                var round = new Round(model.Description, model.Date, model.RankingId);
                var result = await _roundService.UpdateAsync(id, round);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet("{rankingId}")]
        public async Task<IActionResult> GetAsync(Guid rankingId)
        {
            var roundsDTO = new List<RoundDTO>();
            var rounds = await _roundService.GetRoundByRankingIdAsync(rankingId);
            foreach (var round in rounds)
            {
                RoundAdapter.ToRoundDTO(round);
            }

            return Response(roundsDTO);
        }

        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostPunctuationAsync([FromBody] RoundPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                var result = await _roundPunctuationService.AddAsync(roundPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("Punctuation/{id}")]
        public async Task<IActionResult> PutPunctuationAsync(Guid id, [FromBody] RoundPunctuationInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id da pontuação da rodada não foi informado.");

                return Response();
            }

            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                var result = await _roundPunctuationService.UpdateAsync(id, roundPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }
    }
}
