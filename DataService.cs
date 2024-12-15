using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GiftNotation.Data;

namespace GiftNotation.Services.UniversalService
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private readonly GiftNotationDbContext _context;

        public DataService(GiftNotationDbContext context)
        {
            _context = context;
        }

        public async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var deletedEntity = await _context.Set<T>().FindAsync(id);

            if (deletedEntity != null)
            {
                _context.Set<T>().Remove(deletedEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> Update(int id, T entity)
        {
            var entityToUpdate = await _context.Set<T>().FindAsync(id);

            if (entityToUpdate == null)
            {
                return false;
            }

            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
