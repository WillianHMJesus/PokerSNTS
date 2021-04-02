using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
using System.Collections.Generic;
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

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var regulationsDTO = new List<RegulationDTO>();
            var regulations = await _regulationService.GetAll();
            foreach (var regulation in regulations)
            {
                regulationsDTO.Add(new RegulationDTO(regulation));
            }

            return Response(regulationsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RegulationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var regulation = new Regulation(model.Description);
                var result = await _regulationService.Add(regulation);

                return Response(result);
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
                var regulation = new Regulation(model.Description);
                var result = await _regulationService.Update(id, regulation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }
    }
}
