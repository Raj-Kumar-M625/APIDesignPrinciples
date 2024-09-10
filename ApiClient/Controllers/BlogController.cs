using ApiClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController(IBlogApi blogApi) : ControllerBase
    {
        private readonly IBlogApi blogApi = blogApi;

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
           var response = await blogApi.GetPostsAsync();
           return Ok(response);
        }
    }
}
