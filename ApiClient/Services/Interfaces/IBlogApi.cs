﻿using Refit;

namespace ApiClient.Services.Interfaces
{
    public interface IBlogApi
    {
        [Get("/posts/{id}")]
        Task<Post> GetPostAsync(int id);

        [Get("/posts")]
        Task<List<Post>> GetPostsAsync();

        [Post("/posts")]
        Task<Post> CreatePostAsync([Body] Post post);

        [Put("/posts/{id}")]
        Task<Post> UpdatePostAsync(int id, [Body] Post post);

        [Delete("/posts/{id}")]
        Task DeletePostAsync(int id);
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }
}
