using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _ipostRespository;
        public PostService(IPostRepository ipostRepository)
        {
            _ipostRespository = ipostRepository;
        }
        public async Task crear(Post post)
        {
             await _ipostRespository.crear(post);
        }
        public async Task<bool> DeletPost(int id)
        {
            return await _ipostRespository.DeletPost(id);
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _ipostRespository.GetPosts();
        }
        public async Task<Post> GetPosts(int id)
        {
            return await _ipostRespository.GetPosts(id);
        }
        public async Task<bool> updatePost(Post post)
        {
            return await _ipostRespository.updatePost(post);
        }
    }
}
