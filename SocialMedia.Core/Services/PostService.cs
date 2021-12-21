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
        private readonly IPostRepository _postRespository;
        private readonly IUserRepository _userRepository;
        public PostService(IPostRepository ipostRepository, IUserRepository iuserRepository)
        {
            _postRespository = ipostRepository;
            _userRepository = iuserRepository;
        }
        public async Task crear(Post post)
        {
            var user = await _userRepository.GetUser(post.UserId);
            if (user==null)
            {
                throw new Exception("Usuario no existe");
            }
            if (post.Description.Contains("Sexo")==true)
            {
                throw new Exception("Contenido no permitido");
            }
             await _postRespository.crear(post);
        }
        public async Task<bool> DeletPost(int id)
        {
            return await _postRespository.DeletPost(id);
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await _postRespository.GetPosts();
        }
        public async Task<Post> GetPosts(int id)
        {
            return await _postRespository.GetPosts(id);
        }
        public async Task<bool> updatePost(Post post)
        {
            return await _postRespository.updatePost(post);
        }
    }
}
