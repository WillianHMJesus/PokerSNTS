using Microsoft.AspNetCore.Mvc;
using PokerSNTS.API.InputModels;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Notifications;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankingController : BaseController
    {
        private readonly IRankingService _rankingService;

        public RankingController(IRankingService rankingService,
            IDomainNotificationHandler notification) : base(notification)
        {
            _rankingService = rankingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Response(await _rankingService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]RankingInputModel model)
        {
            if (ModelState.IsValid)
            {
                var ranking = new Ranking(model.Description, model.AwardValue);
                var result = await _rankingService.Add(ranking);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]RankingInputModel model)
        {
            if(id.Equals(default(Guid)))
            {
                AddError("O Id do ranking não foi informada.");

                return Response();
            }

            if (ModelState.IsValid)
            {
                var ranking = new Ranking(model.Description, model.AwardValue);
                var result = await _rankingService.Update(id, ranking);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }
    }
}
