using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using MVCTaskEF;

namespace MVCTask.Models.Game
{
    public class CommentsViewModel
    {
        public IEnumerable<Comment> AllComments { get; set; }

        //properties for adding comment
        [Display(Name = "Parent comment key")]
        public string ParentCommentKey { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Comment is required")]
        [Display(Name = "Comment")]
        public string Body { get; set; }
        [Required]
        public string GameKey { get; set; }
    }
}