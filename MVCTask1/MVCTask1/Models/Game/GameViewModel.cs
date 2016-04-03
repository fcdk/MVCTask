using System.ComponentModel.DataAnnotations;

namespace MVCTask1.Models.Game
{
    public class GameViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
