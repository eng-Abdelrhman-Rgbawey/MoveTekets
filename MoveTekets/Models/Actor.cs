global using MoveTekets.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Models
{
    public class Actor : IEntityBase
    {
        public int id { get; set; }
        public string? ProfilePicture { get; set; }

        //[Required(ErrorMessage = "Please upload an image")]
        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile ProfilePictureFile { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength =3 ,ErrorMessage = "Full Name must be between 3 and 50 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        [Display(Name = "Biography")]
        [StringLength(200, ErrorMessage = "Biography cannot be longer than 200 characters")]
        public string Bio { get; set; }


        // Relationships
        public List<ActorMovie>? ActorMovies { get; set; }
    }
}
//  /images/Actors/Actor1.jpeg

