using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.Adapters;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
using System.Linq;
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
                var round = await _roundService.AddAsync(new Round(model.Description, model.Date, model.RankingId));
                if (round == null) return Response();
                var roundDTO = RoundAdapter.ToRoundDTO(round);

                return Response(roundDTO);
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
                var round = await _roundService.UpdateAsync(id, new Round(model.Description, model.Date, model.RankingId));
                if (round == null) return Response();
                var roundDTO = RoundAdapter.ToRoundDTO(round);

                return Response(roundDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet("{rankingId}")]
        public async Task<IActionResult> GetAsync(Guid rankingId)
        {
            var rounds = await _roundService.GetRoundByRankingIdAsync(rankingId);
            var roundsDTO = rounds.Select(x => RoundAdapter.ToRoundDTO(x)).ToList();

            return Response(roundsDTO);
        }

        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostPunctuationAsync([FromBody] RoundPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var roundPunctuation = await _roundPunctuationService.AddAsync(
                    new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId));
                if (roundPunctuation == null) return Response();
                var roundPunctuationDTO = RoundPunctuationAdapter.ToRoundPunctuationDTO(roundPunctuation);

                return Response(roundPunctuationDTO);
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
                var roundPunctuation = await _roundPunctuationService.UpdateAsync(id,
                    new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId));
                if (roundPunctuation == null) return Response();
                var roundPunctuationDTO = RoundPunctuationAdapter.ToRoundPunctuationDTO(roundPunctuation);

                return Response(roundPunctuationDTO);
            }

            NotifyModelStateError();

            return Response();
        }
    }
}
