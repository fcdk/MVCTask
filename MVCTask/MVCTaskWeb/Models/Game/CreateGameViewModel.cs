using System;
using System.ComponentModel.DataAnnotations;

namespace MVCTask.Models.Game
{
    public class CreateGameViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Price")]
        public decimal? Price { get; set; }
        [Display(Name = "Units in stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Discontinued")]
        public bool? Discontinued { get; set; }
    }
}
