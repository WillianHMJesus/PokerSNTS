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
            IDomainNotificationHandler notifications)
            : base(notifications)
        {
            _regulationService = regulationService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegulationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var regulation = await _regulationService.AddAsync(new Regulation(model.Description));
                if (regulation == null) return Response();
                var regulationDTO = RegulationAdapter.ToRegulationDTO(regulation);

                return Response(regulationDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RegulationInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id do regulamento não foi informado.");
                return Response();
            }

            if (ModelState.IsValid)
            {
                var regulation = await _regulationService.UpdateAsync(id, new Regulation(model.Description));
                if (regulation == null) return Response();
                var regulationDTO = RegulationAdapter.ToRegulationDTO(regulation);

                return Response(regulationDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var regulations = await _regulationService.GetAllAsync();
            var regulationsDTO = regulations.Select(x => RegulationAdapter.ToRegulationDTO(x)).ToList();

            return Response(regulationsDTO);
        }
    }
}
