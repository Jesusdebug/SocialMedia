using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
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
        public  IEnumerable<Post> GetAllPost()
        {
            return _unitOfWork.PostRepository.GetAll();
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
                throw new BusinessException("Usuario no existe");
            }
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count()<10)
            {
                var lastPost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastPost.Date).TotalDays <7)
                {
                    throw new BusinessException("No esta disponible para hacer la publicacion");
                }
            }
            if (post.Description.Contains("Sexo")==true)
            {
                throw new BusinessException("Contenido no permitido");
            }
             await _unitOfWork.PostRepository.Add(post);
           await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> UpdatePost(Post post)
        {
            _unitOfWork.PostRepository.update(post);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeletePost(int id)
        {
            return await _unitOfWork.PostRepository.Delete(id);
        }

      
    }
}
