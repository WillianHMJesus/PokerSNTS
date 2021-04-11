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
    public class PlayerController : BaseController
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService,
            INotificationHandler notifications)
            : base(notifications)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PlayerInputModel model)
        {
            if (ModelState.IsValid)
            {
                var player = new Player(model.Name);
                await _playerService.AddAsync(player);

                if (ValidOperation())
                    return Created(GetRouteById(player.Id), new { id = player.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] PlayerInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id do jogador não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var player = new Player(model.Name);
                await _playerService.UpdateAsync(id, player);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var players = await _playerService.GetAllAsync();

            if (players.Any())
                return Ok(players.Select(x => PlayerAdapter.ToPlayerDTO(x)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var player = await _playerService.GetByIdAsync(id);

            if (player != null)
                return Ok(PlayerAdapter.ToPlayerDTO(player));

            return NoContent();
        }
    }
}
