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
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(SocialMediaContext context) : base(context) { }
      

          async Task<IEnumerable<Post>> IPostRepository.GetPostsByUser(int UserId)
        {
            return await _entities.Where(x => x.UserId == UserId).ToListAsync();
        }
    }
}
