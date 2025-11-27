using System.ComponentModel.DataAnnotations;

namespace GameArena.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Password { get; set; }

        [Required]
        public string Username { get; set; }

        public int Score { get; set; } = 0;
    }
}
