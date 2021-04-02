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

        #region Ranking
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] RankingInputModel model)
        {
            if (ModelState.IsValid)
            {
                var ranking = new Ranking(model.Description, model.AwardValue);
                var result = await _rankingService.AddAsync(ranking);

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
                var result = await _rankingService.UpdateAsync(id, ranking);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var rankingDTO = new List<RankingDTO>();
            var rankings = await _rankingService.GetAllAsync();
            foreach (var ranking in rankings)
            {
                rankingDTO.Add(RankingAdapter.ToRankingDTO(ranking));
            }

            return Response(rankingDTO);
        }
        #endregion

        #region Punctuation
        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostAsync([FromBody] RankingPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var rankingPunctuation = new RankingPunctuation(model.Position, model.Punctuation);
                var result = await _rankingPunctuationService.AddAsync(rankingPunctuation);

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
                var result = await _rankingPunctuationService.UpdateAsync(id, rankingPunctuation);

                return Response(result);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet("Punctuation")]
        public async Task<IActionResult> GetPunctuationAsync()
        {
            var rankingPunctuationsDTO = new List<RankingPunctuationDTO>();
            var rankingPunctuations = await _rankingPunctuationService.GetAllAsync();
            foreach (var rankingPunctuation in rankingPunctuations)
            {
                rankingPunctuationsDTO.Add(RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation));
                                           
            }

            return Response(rankingPunctuationsDTO);
        }

        [HttpGet("Punctuation/{position}")]
        public async Task<IActionResult> GetPunctuationByPositionAsync(short position)
        {
            var rankingPunctuation = await _rankingPunctuationService.GetRankingPunctuationByPositionAsync(position);
            if (rankingPunctuation == null) return Response();
            var rankingPunctuationDTO = RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation);

            return Response(rankingPunctuationDTO);
        }
        #endregion
    }
}
