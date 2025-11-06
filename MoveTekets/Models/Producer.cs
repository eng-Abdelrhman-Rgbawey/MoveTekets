namespace WebApplication1.Models
{
    public class Producer
    {
        public int id { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public string Bio { get; set; }

        // Relationships
        public List<Move> Moves { get; set; }
    }
}
