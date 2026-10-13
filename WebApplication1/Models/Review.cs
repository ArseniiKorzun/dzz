namespace WebApplication1.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Author { get; set; } = "";
        public string Text { get; set; } = "";
        public int Rating { get; set; }

        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
