using System.ComponentModel.DataAnnotations;

namespace MVCTask.Models.Order
{
    public class OrderDetailsViewModel
    {
        [Required]
        public string GameKey { get; set; }
        public string GameName { get; set; }
        [Range(1, 32767, ErrorMessage = "Quantity value must be from 1 to 32767")]
        [Required(ErrorMessage = "Quantity is required")]
        public short Quantity { get; set; }
        public float? Discount { get; set; }
    }
}
