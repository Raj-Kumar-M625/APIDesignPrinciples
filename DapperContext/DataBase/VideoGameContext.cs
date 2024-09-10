using DapperContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace DapperContext.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options) { }
        public DbSet<VideoGame> VideoGame { get; set; }
    }
}
