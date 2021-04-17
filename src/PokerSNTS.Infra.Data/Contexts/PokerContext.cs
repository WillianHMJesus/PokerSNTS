using Microsoft.EntityFrameworkCore;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Contexts
{
    public sealed class PokerContext : DbContext
    {
        public PokerContext(DbContextOptions<PokerContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<RoundPoint> RoundsPoints { get; set; }
        public DbSet<Ranking> Ranking { get; set; }
        public DbSet<RankingPoint> RankingPoints { get; set; }
        public DbSet<Regulation> Regulations { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
