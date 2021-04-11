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
            INotificationHandler notifications)
            : base(notifications)
        {
            _roundService = roundService;
            _roundPunctuationService = roundPunctuationService;
        }

        #region Round
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RoundInputModel model)
        {
            if (ModelState.IsValid)
            {
                var round = new Round(model.Description, model.Date, model.RankingId);
                await _roundService.AddAsync(round);

                if (ValidOperation())
                    return Created(GetRouteById(round.Id), new { id = round.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RoundInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id da rodada não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var round = new Round(model.Description, model.Date, model.RankingId);
                await _roundService.UpdateAsync(id, round);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var rounds = await _roundService.GetAllAsync();

            if (rounds.Any())
                return Ok(rounds.Select(x => RoundAdapter.ToRoundDTO(x)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var round = await _roundService.GetByIdAsync(id);

            if (round != null)
                return Ok(RoundAdapter.ToRoundDTO(round));

            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByRankingIdAsync([FromQuery]Guid rankingId)
        {
            var rounds = await _roundService.GetByRankingIdAsync(rankingId);

            if (rounds.Any())
                return Ok(rounds.Select(x => RoundAdapter.ToRoundDTO(x)));

            return NoContent();
        }
        #endregion

        #region Round Punctuation
        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostPunctuationAsync([FromBody] RoundPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                await _roundPunctuationService.AddAsync(roundPunctuation);

                if (ValidOperation())
                    return Created(GetRouteById(roundPunctuation.Id), new { id = roundPunctuation.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("Punctuation/{id}")]
        public async Task<IActionResult> PutPunctuationAsync(Guid id, [FromBody] RoundPunctuationInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id da pontuação da rodada não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var roundPunctuation = new RoundPunctuation(model.Position, model.Punctuation, model.PlayerId, model.RoundId);
                await _roundPunctuationService.UpdateAsync(id, roundPunctuation);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet("Punctuation")]
        public async Task<IActionResult> GetPunctuationAsync()
        {
            var roundsPunctuations = await _roundPunctuationService.GetAllAsync();

            if (roundsPunctuations.Any())
                return Ok(roundsPunctuations.Select(x => RoundPunctuationAdapter.ToRoundPunctuationDTO(x)));

            return NoContent();
        }

        [HttpGet("Punctuation/{id}")]
        public async Task<IActionResult> GetPunctuationByIdAsync(Guid id)
        {
            var roundPunctuation = await _roundPunctuationService.GetByIdAsync(id);

            if (roundPunctuation != null)
                return Ok(RoundPunctuationAdapter.ToRoundPunctuationDTO(roundPunctuation));

            return NoContent();
        }
        #endregion
    }
}
