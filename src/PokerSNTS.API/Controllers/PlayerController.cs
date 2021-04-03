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
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _playerService = playerService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PlayerInputModel model)
        {
            if (ModelState.IsValid)
            {
                var player = await _playerService.AddAsync(new Player(model.Name));
                if (player == null) return Response();
                var playerDTO = PlayerAdapter.ToPlayerDTO(player);

                return Response(playerDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] PlayerInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id do jogador não foi informado.");
                return Response();
            }

            if (ModelState.IsValid)
            {
                var player = await _playerService.UpdateAsync(id, new Player(model.Name));
                if (player == null) return Response();
                var playerDTO = PlayerAdapter.ToPlayerDTO(player);

                return Response(playerDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var players = await _playerService.GetAllAsync();
            var playersDTO = players.Select(x => PlayerAdapter.ToPlayerDTO(x)).ToList();

            return Response(playersDTO);
        }
    }
}
