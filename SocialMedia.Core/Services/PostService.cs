using Microsoft.Extensions.Options;
using SocialMedia.Core.CustormerEntities;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
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
        private readonly PaginationOptions _options;
        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options)
        {
            _unitOfWork= unitOfWork;
            _options = options.Value;
        }
        public PagedList<Post> GetAllPost(PostQueryFilter filter)
        {
            //Filter.PageNumber = Filter.PageNumber == 0 ? 1 : Filter.PageNumber;
            //Filter.PageZise = Filter.PageZise ==0 ? 2 : Filter.PageZise;
            filter.PageNumber = filter.PageNumber == 0 ? _options.DefaulPageNumber : filter.PageNumber;
            filter.PageZise = filter.PageZise == 0 ? _options.DefaulPageSize : filter.PageZise;

            var posts= _unitOfWork.PostRepository.GetAll();

            if (filter.UserId!=null)
            {
                posts = posts.Where(x => x.UserId == filter.UserId);
            }
            if (filter.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filter.Date?.ToShortDateString());
            }
            if (filter.Description !=null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
            }
            var pagedPosts = PagedList<Post>.create(posts,filter.PageNumber, filter.PageZise);
            return pagedPosts;   
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
