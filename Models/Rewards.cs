using System.ComponentModel.DataAnnotations;

namespace GameArena.Models
{
    public class Reward
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Cost { get; set; }

        public string Description { get; set; }
        public string ImageFileName { get; set; }

    }
}
