using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using System.Threading.Tasks;

namespace PokerSNTS.Infra.Data.Contexts
{
    public sealed class PokerContext : DbContext, IUnitOfWork
    {
        public PokerContext(DbContextOptions<PokerContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerRound> PlayersRounds { get; set; }
        public DbSet<Ranking> Ranking { get; set; }
        public DbSet<RankingPunctuation> RankingPunctuations { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.AddIgnored("ValidationResult");
                entity.AddIgnored("IsValid");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
