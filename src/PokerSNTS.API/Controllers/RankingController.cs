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
    [ApiController]
    public class RankingController : BaseController
    {
        private readonly IRankingService _rankingService;
        private readonly IRankingPunctuationService _rankingPunctuationService;

        public RankingController(IRankingService rankingService,
            IRankingPunctuationService rankingPunctuationService,
            INotificationHandler notification) : base(notification)
        {
            _rankingService = rankingService;
            _rankingPunctuationService = rankingPunctuationService;
        }

        #region Ranking
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RankingInputModel model)
        {
            if (ModelState.IsValid)
            {
                var ranking = new Ranking(model.Description, model.AwardValue);
                await _rankingService.AddAsync(ranking);

                if (ValidOperation())
                    return Created(GetRouteById(ranking.Id), new { id = ranking.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RankingInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id do ranking não foi informada.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var ranking = new Ranking(model.Description, model.AwardValue);
                await _rankingService.UpdateAsync(id, ranking);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var ranking = await _rankingService.GetAllAsync();

            if (ranking.Any())
                return Ok(ranking.Select(x => RankingAdapter.ToRankingDTO(x)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var ranking = await _rankingService.GetByIdAsync(id);

            if (ranking != null)
                return Ok(RankingAdapter.ToRankingDTO(ranking));

            return NoContent();
        }

        [HttpGet("{id}/Overrall")]
        public async Task<IActionResult> GetOverrallByIdAsync(Guid id)
        {
            var ranking = await _rankingService.GetOverallById(id);

            if (ranking.Any())
                return Ok(ranking);

            return NoContent();
        }

        [HttpGet("Overrall/{initialDate=initialDate}/{finalDate=finalDate}")]
        public async Task<IActionResult> GetOverrallByPeriodAsync(DateTime initialDate, DateTime finalDate)
        {
            var ranking = await _rankingService.GetOverallByPeriod(initialDate, finalDate);

            if (ranking.Any())
                return Ok(ranking);

            return NoContent();
        }
        #endregion

        #region Ranking Punctuation
        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostAsync([FromBody] RankingPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var rankingPunctuation = new RankingPunctuation(model.Position, model.Punctuation);
                await _rankingPunctuationService.AddAsync(rankingPunctuation);

                if (ValidOperation())
                    return Created(GetRouteById(rankingPunctuation.Id), new { id = rankingPunctuation.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("Punctuation/{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RankingPunctuationInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id da pontuação do ranking não foi informada.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var rankingPunctuation = new RankingPunctuation(model.Position, model.Punctuation);
                await _rankingPunctuationService.UpdateAsync(id, rankingPunctuation);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet("Punctuation")]
        public async Task<IActionResult> GetPunctuationAsync()
        {
            var rankingPunctuations = await _rankingPunctuationService.GetAllAsync();

            if (rankingPunctuations.Any())
                return Ok(rankingPunctuations.Select(x => RankingPunctuationAdapter.ToRankingPunctuationDTO(x)));

            return NoContent();
        }

        [HttpGet("Punctuation/{id}")]
        public async Task<IActionResult> GetPunctuationByIdAsync(Guid id)
        {
            var rankingPunctuation = await _rankingPunctuationService.GetByIdAsync(id);

            if (rankingPunctuation != null)
                return Ok(RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation));

            return NoContent();
        }

        [HttpGet("Punctuation/filter")]
        public async Task<IActionResult> GetPunctuationByPositionAsync([FromQuery]short position)
        {
            var rankingPunctuation = await _rankingPunctuationService.GetByPositionAsync(position);

            if (rankingPunctuation != null)
                return Ok(RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation));

            return NoContent();
        }
        #endregion
    }
}
