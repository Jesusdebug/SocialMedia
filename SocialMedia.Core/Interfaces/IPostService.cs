using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetAllPost();
        Task<Post> GetPostById(int id);
        Task CreatePost(Post post);
        Task<bool> UpdatePost(Post post);
        Task<bool> DeletePost(int id);
    }
}