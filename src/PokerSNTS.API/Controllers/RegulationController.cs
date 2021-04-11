using Microsoft.AspNetCore.Authorization;
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
    public class RegulationController : BaseController
    {
        private readonly IRegulationService _regulationService;

        public RegulationController(IRegulationService regulationService,
            INotificationHandler notifications)
            : base(notifications)
        {
            _regulationService = regulationService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegulationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var regulation = new Regulation(model.Description);
                await _regulationService.AddAsync(regulation);

                if (ValidOperation())
                    return Created(GetRouteById(regulation.Id), new { id = regulation.Id });

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RegulationInputModel model)
        {
            if (default(Guid).Equals(id))
            {
                AddNotification("O Id do regulamento não foi informado.");
                return ResponseInvalid();
            }

            if (ModelState.IsValid)
            {
                var regulation = new Regulation(model.Description);
                await _regulationService.UpdateAsync(id, regulation);

                if (ValidOperation()) return NoContent();

                return ResponseInvalid();
            }

            NotifyModelStateError();

            return ResponseInvalid();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync()
        {
            var regulations = await _regulationService.GetAllAsync();

            if (regulations.Any())
                return Ok(regulations.Select(x => RegulationAdapter.ToRegulationDTO(x)));

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var regulation = await _regulationService.GetByIdAsync(id);

            if (regulation != null)
                return Ok(RegulationAdapter.ToRegulationDTO(regulation));

            return NoContent();
        }
    }
}
