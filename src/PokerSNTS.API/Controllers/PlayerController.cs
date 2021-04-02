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
                var player = new Player(model.Name);
                var result = await _playerService.AddAsync(player);

                return Response(result);
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
                var player = new Player(model.Name);
                var result = await _playerService.UpdateAsync(id, player);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var playersDTO = new List<PlayerDTO>();
            var players = await _playerService.GetAllAsync();
            foreach (var player in players)
            {
                playersDTO.Add(PlayerAdapter.ToPlayerDTO(player));
            }

            return Response();
        }
    }
}
