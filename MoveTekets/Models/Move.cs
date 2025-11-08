using WebApplication1.Data.Enums;

namespace WebApplication1.Models
{
    public class Move : IEntityBase
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? ImageURL { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; } // Enum

        // Relationships
        public List<ActorMovie> ActorMovies { get; set; } = new List<ActorMovie>();

        //Cinema
        public int CinemaId { get; set; } 
        public Cinema Cinema { get; set; } = new Cinema();

        //Producer
        public int ProducerId { get; set; }
        public Producer Producer { get; set; } = new Producer();

    }
}
