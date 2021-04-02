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
    [ApiController]
    public class RankingController : BaseController
    {
        private readonly IRankingService _rankingService;
        private readonly IRankingPunctuationService _rankingPunctuationService;

        public RankingController(IRankingService rankingService,
            IRankingPunctuationService rankingPunctuationService,
            IDomainNotificationHandler notification) : base(notification)
        {
            _rankingService = rankingService;
            _rankingPunctuationService = rankingPunctuationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Response(await _rankingService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RankingInputModel model)
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
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RankingInputModel model)
        {
            if (id.Equals(default(Guid)))
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

        [HttpGet("Punctuation")]
        public async Task<IActionResult> GetPunctuationAsync()
        {
            var rankingPunctuationsDTO = new List<RankingPunctuationDTO>();
            var rankingPunctuations = await _rankingPunctuationService.GetAll();
            foreach (var rankingPunctuation in rankingPunctuations)
            {
                rankingPunctuationsDTO.Add(new RankingPunctuationDTO(rankingPunctuation));
            }

            return Response(rankingPunctuationsDTO);
        }

        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostAsync([FromBody] RankingPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var rankingPunctuation = new RankingPunctuation(model.Position, model.Punctuation);
                var result = await _rankingPunctuationService.Add(rankingPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpPut("Punctuation/{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] RankingPunctuationInputModel model)
        {
            if (id.Equals(default(Guid)))
            {
                AddError("O Id da pontuação do ranking não foi informada.");

                return Response();
            }

            if (ModelState.IsValid)
            {
                var rankingPunctuation = new RankingPunctuation(model.Position, model.Punctuation);
                var result = await _rankingPunctuationService.Update(id, rankingPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet("Punctuation/{position}")]
        public async Task<IActionResult> GetPunctuationByPositionAsync(short position)
        {
            var rankingPunctuation = await _rankingPunctuationService.GetRankingPunctuationByPosition(position);
            if (rankingPunctuation == null) return Response();
            var rankingPunctuationDTO = new RankingPunctuationDTO(rankingPunctuation);

            return Response(rankingPunctuationDTO);
        }
    }
}
