using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositorios
{
    public class PostRepository : IPostRepository
    {
        private SocialMediaContext _context;
        public PostRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var post = await _context.Post.ToListAsync();
            return post;
        }
        public async Task<Post> GetPosts(int id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }
        public async Task crear(Post post)
        {
            _context.Post.Add(post);
             _context.SaveChanges();
        }
        public  async Task<bool> updatePost(Post post)
        {
            var currentPost = await GetPosts(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            //url
            currentPost.Image = post.Image;
            int rows= await _context.SaveChangesAsync();
            //si se genera almenos un cambio devolvemos un true
            return rows > 0;
        }
        public async Task<bool> DeletPost(int id)
        {
            var currentPost = await GetPosts(id);
            _context.Post.Remove(currentPost);
            int rows = await _context.SaveChangesAsync();
            //si se genera almenos un cambio devolvemos un true
            return rows > 0;
        }

       
    }
}
