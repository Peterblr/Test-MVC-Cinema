using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title cannot be longer than 50 characters and less 3.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Descriptipn cannot be longer than 500 characters and less 3.")]
        public string Description { get; set; }


        [Required]
        [ForeignKey("Actors")]
        public int ActorId { get; set; }
        public virtual Actor Actors { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
