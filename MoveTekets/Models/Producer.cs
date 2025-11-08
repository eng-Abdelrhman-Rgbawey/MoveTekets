using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Producer : IEntityBase
    {
        public int id { get; set; }
        public string? ProfilePicture { get; set; }

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }



        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 characters")]
        public string FullName { get; set; }



        [Required(ErrorMessage = "Biography is required")]
        [Display(Name = "Biography")]
        [StringLength(200, ErrorMessage = "Biography cannot be longer than 200 characters")]
        public string Bio { get; set; }

        // Relationships
        public List<Move>? Moves { get; set; }
    }
}
