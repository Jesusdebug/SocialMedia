using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositorios
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(SocialMediaContext context) : base(context)
        {
        }
        public async Task<Security> GetLoginByCredencials(UserLoging loging)
        {
            return await _entities.FirstOrDefaultAsync(x => x.User == loging.user);
        }
    }
}
