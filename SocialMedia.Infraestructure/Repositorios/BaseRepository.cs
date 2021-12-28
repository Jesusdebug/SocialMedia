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
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext _context;
        protected DbSet<T> _entities;
        public BaseRepository(SocialMediaContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return  _entities.AsEnumerable();
        }
        public async Task<T> GetById(int id)
        {
            return await _entities.FindAsync(id);
        }
        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void update(T entity)
        {
            _entities.Update(entity);
        }
        public async Task<bool> Delete(int id)
        {
            T enity = await GetById(id);
            _entities.Remove(enity);
            return true;
        }
    }
}
