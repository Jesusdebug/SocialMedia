using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPosts();
        Task<Post> GetPosts(int id);
        Task crear(Post post);
        Task<bool> updatePost(Post post);
        Task<bool> DeletPost(int id);
    }
}