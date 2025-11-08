
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        // Inject AppDbContext 
        private readonly AppDbContext context;
        public EntityBaseRepository(AppDbContext _context)
        {
            context = _context;
        }
        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.id == id);
            if (entity != null)
            {
                EntityEntry entityEntry = context.Entry(entity);
                entityEntry.State = EntityState.Deleted;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await context.Set<T>().ToListAsync();
            return result;
        }

        public async Task<T> GetById(int id)
        {
            var result = await context.Set<T>().FirstOrDefaultAsync(a => a.id == id);
            return result;
        }


        public async Task Update(int id, T entity)
        {
            EntityEntry entityEntry = context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
