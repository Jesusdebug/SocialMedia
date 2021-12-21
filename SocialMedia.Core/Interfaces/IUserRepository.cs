using SocialMedia.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserRepository
    {
        Task crear(User post);
        Task<bool> DeletPost(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);
      
    }
}