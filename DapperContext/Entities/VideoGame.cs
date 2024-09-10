namespace DapperContext.Entities
{
    public class VideoGame
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Publisher { get; set; }
        public required string Developer { get; set; }
        public DateTime ReleasedDate { get; set; }
    }
}
