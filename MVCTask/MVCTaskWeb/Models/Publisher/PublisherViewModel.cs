using System.ComponentModel.DataAnnotations;

namespace MVCTask.Models.Publisher
{
    public class PublisherViewModel
    {
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Home page")]
        public string HomePage { get; set; }
    }
}