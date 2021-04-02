using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
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

        [HttpGet("{rankingId}")]
        public async Task<IActionResult> GetAsync(Guid rankingId)
        {
            return Response(await _roundService.GetRoundByRankingId(rankingId));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RoundInputModel model)
        {
            if (ModelState.IsValid)
            {
                var round = new Round(model.Description, model.Date, model.RankingId);
                var result = await _roundService.Add(round);

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
                var result = await _roundService.Update(id, round);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostPunctuation([FromBody] RoundPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                var result = await _roundPunctuationService.Add(roundPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("Punctuation/{id}")]
        public async Task<IActionResult> PutPunctuation(Guid id, [FromBody] RoundPunctuationInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id da pontuação da rodada não foi informado.");

                return Response();
            }

            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                var result = await _roundPunctuationService.Update(id, roundPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }
    }
}
