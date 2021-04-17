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
        private readonly IRankingPointService _rankingPointService;

        public RankingController(IRankingService rankingService,
            IRankingPointService rankingPointService,
            INotificationHandler notification) : base(notification)
        {
            _rankingService = rankingService;
            _rankingPointService = rankingPointService;
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

            if (ranking != null)
                return Ok(RankingAdapter.ToRankingOverallDTO(ranking));

            return NoContent();
        }
        #endregion

        #region Ranking Point
        [HttpPost("Point")]
        public async Task<IActionResult> PostAsync([FromBody] RankingPointInputModel model)
        {
            if (ModelState.IsValid)
            {
                var rankingPoint = new RankingPoint(model.Position, model.Point);
                await _rankingPointService.AddAsync(rankingPoint);

                if (ValidOperation())
                    return Created(GetRouteById(rankingPoint.Id), new { id = rankingPoint.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("Point/{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RankingPointInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id da pontuação do ranking não foi informada.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var rankingPoint = new RankingPoint(model.Position, model.Point);
                await _rankingPointService.UpdateAsync(id, rankingPoint);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet("Point")]
        public async Task<IActionResult> GetPointAsync()
        {
            var rankingPoints = await _rankingPointService.GetAllAsync();

            if (rankingPoints.Any())
                return Ok(rankingPoints.Select(x => RankingPointAdapter.ToRankingPointDTO(x)));

            return NoContent();
        }

        [HttpGet("Point/{id}")]
        public async Task<IActionResult> GetPointByIdAsync(Guid id)
        {
            var rankingPoint = await _rankingPointService.GetByIdAsync(id);

            if (rankingPoint != null)
                return Ok(RankingPointAdapter.ToRankingPointDTO(rankingPoint));

            return NoContent();
        }

        [HttpGet("Point/filter")]
        public async Task<IActionResult> GetPointByPositionAsync([FromQuery]short position)
        {
            var rankingPoint = await _rankingPointService.GetByPositionAsync(position);

            if (rankingPoint != null)
                return Ok(RankingPointAdapter.ToRankingPointDTO(rankingPoint));

            return NoContent();
        }
        #endregion
    }
}
