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
        private readonly IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }
        public async Task<IEnumerable<Post>> GetAllPost()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }
        public async Task<Post> GetPostById(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }
        public async Task CreatePost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user==null)
            {
                throw new Exception("Usuario no existe");
            }
            if (post.Description.Contains("Sexo")==true)
            {
                throw new Exception("Contenido no permitido");
            }
             await _unitOfWork.PostRepository.Add(post);
        }
        public async Task<bool> UpdatePost(Post post)
        {
            return await _unitOfWork.PostRepository.update(post);
        }
        public async Task<bool> DeletePost(int id)
        {
            return await _unitOfWork.PostRepository.Delete(id);
        }
    }
}
