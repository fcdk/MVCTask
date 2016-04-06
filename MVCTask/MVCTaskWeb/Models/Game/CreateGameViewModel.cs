using System.ComponentModel.DataAnnotations;

namespace MVCTask.Models.Game
{
    public class CreateGameViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
