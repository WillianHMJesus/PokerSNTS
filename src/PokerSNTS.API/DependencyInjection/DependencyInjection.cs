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
            services.AddScoped<INotificationHandler, NotificationHandler>();

            //Data
            services.AddScoped<PokerContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IRoundPunctuationRepository, RoundPunctuationRepository>();
            services.AddScoped<IRankingRepository, RankingRepository>();
            services.AddScoped<IRankingPunctuationRepository, RankingPunctuationRepository>();
            services.AddScoped<IRegulationRepository, RegulationRepository>();
            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            //Service
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IRoundPunctuationService, RoundPunctuationService>();
            services.AddScoped<IRankingService, RankingService>();
            services.AddScoped<IRankingPunctuationService, RankingPunctuationService>();
            services.AddScoped<IRegulationService, RegulationService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
