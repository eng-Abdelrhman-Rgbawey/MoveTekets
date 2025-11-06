namespace WebApplication1.Models
{
    public class Cinema
    {
        public int id { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Relationships
        public List<Move> Moves { get; set; }

    }
}
