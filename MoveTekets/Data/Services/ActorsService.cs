using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public class ActorsService : IActorsService
    {
        // Inject AppDbContext
        private readonly AppDbContext context;
        public ActorsService(AppDbContext _context)
        {
            context = _context;
        }

        // Implementing IActorsService
        public void Add(Actor actor)
        {
        }

        public void Delete(int id)
        {
        }

        public async Task<IEnumerable<Actor>> GetAll()
        {
           var Actors = await context.Actors.ToListAsync();
              return Actors;
        }

        public Actor GetById(int id)
        {
            return context.Actors.Find(id);
        }

        public void Update(int id, Actor newActor)
        {
        }
    }
}
