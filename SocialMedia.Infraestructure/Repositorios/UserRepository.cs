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
    public class UserRepository : IUserRepository
    {
        private SocialMediaContext _context;
        public UserRepository(SocialMediaContext context)
        {
            _context = context;
        }
        public Task crear(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletPost(int id)
        {
            throw new NotImplementedException();
        }

        public  async Task<User> GetUser(int id)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return usuario;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var usuario = await _context.Users.ToListAsync();
            return usuario;
        }
    }
}
