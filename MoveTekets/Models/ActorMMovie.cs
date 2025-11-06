namespace WebApplication1.Models
{
    public class ActorMovie
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }

        public int MovieId { get; set; }
        public Move Movie { get; set; }
    }
}
