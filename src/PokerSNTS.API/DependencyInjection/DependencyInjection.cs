using Microsoft.Extensions.DependencyInjection;
using PokerSNTS.Domain.Interfaces.Repositories;
using PokerSNTS.Domain.Interfaces.Services;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using PokerSNTS.Domain.Notifications;
using PokerSNTS.Domain.Services;
using PokerSNTS.Infra.Data.Contexts;
using PokerSNTS.Infra.Data.Repositories;
using PokerSNTS.Infra.Data.UnitOfWork;

namespace PokerSNTS.API
{
    public static class DependencyInjection
    {
        public static void ResolveDependencyInjection(this IServiceCollection services)
        {
            //Notification
            services.AddScoped<IDomainNotificationHandler, DomainNotificationHandler>();

            //Data
            services.AddScoped<PokerContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayerRoundRepository, PlayerRoundRepository>();
            services.AddScoped<IRankingRepository, RankingRepository>();
            services.AddScoped<IRankingPunctuationRepository, RankingPunctuationRepository>();
            services.AddScoped<IRegulationRepository, RegulationRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();

            //Service
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRoundService, PlayerRoundService>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IRankingPunctuationService, RankingPunctuationService>();
            services.AddScoped<IRegulationService, RegulationService>();
            services.AddScoped<IRoundService, RoundService>();
        }
    }
}
