using Dapper;
using DapperContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;

namespace DapperContext.Services
{
    public class VideoGameService : IVideoGameService
    {
        private readonly IConfiguration _configuration;
        public VideoGameService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task AddAsync(VideoGame videoGame)
        {
            using var connection  = GetConnection();
            await connection.ExecuteAsync(@"INSERT INTO VideoGame (Title,Publisher,Developer,ReleasedDate) 
                                            VALUES (@Title,@Publisher,@Developer,@ReleasedDate)", videoGame);
        }

        public async Task<List<VideoGame>> GetAllAsync()
        {
            using var connection = GetConnection();
            var videoGames = await connection.QueryAsync<VideoGame>("SELECT * FROM VideoGame");
            return videoGames.ToList();
        }
        public async Task<VideoGame> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            var videoGame = await connection.QueryFirstOrDefaultAsync<VideoGame>("SELECT * FROM VideoGame WHERE Id = @id", new { Id = id });
            return videoGame;
        }

        public async Task UpdateAsync(VideoGame videoGame)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync(@"UPDATE VideoGame 
                                            SET Title = @Title,Publisher = @Publisher,Developer = @Developer, ReleasedDate = @ReleasedDate
                                            Where Id = @Id",videoGame);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("DELETE FROM VideoGame WHERE Id = @id", new { Id = id });
        }
    }
}
