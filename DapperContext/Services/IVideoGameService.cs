using DapperContext.Entities;

namespace DapperContext.Services
{
    public interface IVideoGameService
    {
        Task<List<VideoGame>> GetAllAsync();
        Task<VideoGame> GetByIdAsync(int id);
        Task AddAsync(VideoGame videoGame);
        Task UpdateAsync(VideoGame videoGame); 
        Task DeleteAsync(int id);
    }
}
