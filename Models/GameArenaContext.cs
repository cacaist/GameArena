using Microsoft.EntityFrameworkCore;

namespace GameArena.Models
{
    public class GameArenaContext : DbContext
    {
        public GameArenaContext(DbContextOptions<GameArenaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<UserReward> UserRewards { get; set; }

    }
}
