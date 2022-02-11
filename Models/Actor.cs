using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class Actor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name cannot be longer than 50 characters and less 3.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }

        [StringLength(500, MinimumLength = 3, ErrorMessage = "Biografy cannot be longer than 500 characters and less 3.")]
        public string Biografy { get; set; }
    }
}
