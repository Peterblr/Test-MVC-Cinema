using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please, write comment...")]
        [Display(Name = "Comment"), StringLength(1000, MinimumLength = 3)]
        public string CommentItem { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;

        //user
        public string UserId { get; set; }

        //movie
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
