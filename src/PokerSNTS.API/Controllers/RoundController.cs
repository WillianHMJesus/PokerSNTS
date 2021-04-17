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
        private readonly IRoundPointService _roundPointService;

        public RoundController(IRoundService roundService,
            IRoundPointService roundPointService,
            INotificationHandler notifications)
            : base(notifications)
        {
            _roundService = roundService;
            _roundPointService = roundPointService;
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

        #region Round Point
        [HttpPost("Point")]
        public async Task<IActionResult> PostPointAsync([FromBody] RoundPointInputModel model)
        {
            if (ModelState.IsValid)
            {
                var roundPoint = new RoundPoint(model.Position, model.Point, model.PlayerId, model.RoundId);
                await _roundPointService.AddAsync(roundPoint);

                if (ValidOperation())
                    return Created(GetRouteById(roundPoint.Id), new { id = roundPoint.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("Point/{id}")]
        public async Task<IActionResult> PutPointAsync(Guid id, [FromBody] RoundPointInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id da pontuação da rodada não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var roundPoint = new RoundPoint(model.Position, model.Point, model.PlayerId, model.RoundId);
                await _roundPointService.UpdateAsync(id, roundPoint);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet("Point")]
        public async Task<IActionResult> GetPointAsync()
        {
            var roundsPoints = await _roundPointService.GetAllAsync();

            if (roundsPoints.Any())
                return Ok(roundsPoints.Select(x => RoundPointAdapter.ToRoundPointDTO(x)));

            return NoContent();
        }

        [HttpGet("Point/{id}")]
        public async Task<IActionResult> GetPointByIdAsync(Guid id)
        {
            var roundPoint = await _roundPointService.GetByIdAsync(id);

            if (roundPoint != null)
                return Ok(RoundPointAdapter.ToRoundPointDTO(roundPoint));

            return NoContent();
        }
        #endregion
    }
}
