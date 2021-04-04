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
                var ranking = await _rankingService.AddAsync(new Ranking(model.Description, model.AwardValue));
                if (ranking == null) return Response();
                var rankingDTO = RankingAdapter.ToRankingDTO(ranking);

                return Response(rankingDTO);
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
                var ranking = await _rankingService.UpdateAsync(id, new Ranking(model.Description, model.AwardValue));
                if (ranking == null) return Response();
                var rankingDTO = RankingAdapter.ToRankingDTO(ranking);

                return Response(rankingDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var ranking = await _rankingService.GetAllAsync();
            var rankingDTO = ranking.Select(x => RankingAdapter.ToRankingDTO(x)).ToList();

            return Response(rankingDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOverrallByIdAsync(Guid id)
        {
            return Response(await _rankingService.GetOverallById(id));
        }

        [HttpGet("{initialDate}/{finalDate}")]
        public async Task<IActionResult> GetOverrallByPeriodAsync(DateTime initialDate, DateTime finalDate)
        {
            return Response(await _rankingService.GetOverallByPeriod(initialDate, finalDate));
        }
        #endregion

        #region Punctuation
        [HttpPost("Punctuation")]
        public async Task<IActionResult> PostAsync([FromBody] RankingPunctuationInputModel model)
        {
            if (ModelState.IsValid)
            {
                var rankingPunctuation = await _rankingPunctuationService.AddAsync(new RankingPunctuation(model.Position, model.Punctuation));
                if (rankingPunctuation == null) return Response();
                var rankingPunctuationDTO = RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation);

                return Response(rankingPunctuationDTO);
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
                var rankingPunctuation = await _rankingPunctuationService.UpdateAsync(id, new RankingPunctuation(model.Position, model.Punctuation));
                if (rankingPunctuation == null) return Response();
                var rankingPunctuationDTO = RankingPunctuationAdapter.ToRankingPunctuationDTO(rankingPunctuation);

                return Response(rankingPunctuationDTO);
            }

            NotifyModelStateError();

            return Response();
        }

        [HttpGet("Punctuation")]
        public async Task<IActionResult> GetPunctuationAsync()
        {
            var rankingPunctuations = await _rankingPunctuationService.GetAllAsync();
            var rankingPunctuationsDTO = rankingPunctuations.Select(x => RankingPunctuationAdapter.ToRankingPunctuationDTO(x)).ToList();

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
