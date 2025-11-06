using WebApplication1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication1.Models
{
    public class Actor
    {
        public int id { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }


        // Relationships
        public List<ActorMovie> ActorMovies { get; set; }
    }
}
//  /images/Actors/Actor1.jpeg

