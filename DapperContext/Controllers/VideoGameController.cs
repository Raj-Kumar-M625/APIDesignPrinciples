using DapperContext.Entities;
using DapperContext.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoGameController : ControllerBase
    {
        private readonly IVideoGameService _videoGameService;

        public VideoGameController(IVideoGameService videoGameService)
        {
            _videoGameService = videoGameService;
        }

        [HttpGet("GetAllGames")]
        public async Task<IActionResult> GetAllGames()
        {
            return Ok(await _videoGameService.GetAllAsync());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var videoGame = await _videoGameService.GetByIdAsync(id);
            if (videoGame == null)
            {
                return NotFound("This game does not exist in the DataBase. :(");
            }
            return Ok(videoGame);
        }

        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame(VideoGame videoGame)
        {
            await _videoGameService.AddAsync(videoGame);
            return CreatedAtAction("AddGame", new { id = videoGame.Id }, videoGame);
        }

        [HttpPost("UpdateGame")]
        public async Task<IActionResult> UpdateGame(VideoGame videoGame)
        {
            var existingGame = await _videoGameService.GetByIdAsync(videoGame.Id);
            if (existingGame == null)
            {
                return NotFound("This game does not exist in the DataBase. :(");
            }
            await _videoGameService.UpdateAsync(videoGame);
            return NoContent();
        }

        [HttpDelete("DeleteGame/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var existingGame = await _videoGameService.GetByIdAsync(id);
            if (existingGame == null)
            {
                return NotFound("This game does not exist in the DataBase. :(");
            }
            await _videoGameService.DeleteAsync(id);
            return NoContent();
        }
    }
}
