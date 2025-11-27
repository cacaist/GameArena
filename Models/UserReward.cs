namespace GameArena.Models
{
    public class UserReward
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int RewardId { get; set; }
        public Reward Reward { get; set; }
    }
}
