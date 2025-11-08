using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public class ActorsService : EntityBaseRepository<Actor>, IActorsService
    {
        public ActorsService(AppDbContext _context) : base(_context) { }


        //// Implementing IActorsService
        //public async Task Add(Actor actor)
        //{
        //   await context.Actors.AddAsync(actor);
        //}

        //public async Task Delete(int id)
        //{
        //    var actor = await context.Actors.FirstOrDefaultAsync(a => a.id == id);
        //    if (actor != null)
        //    {
        //        context.Actors.Remove(actor);
        //    }
        //}

        //public async Task<IEnumerable<Actor>> GetAll()
        //{
        //   var Actors = await context.Actors.ToListAsync();
        //      return Actors;
        //}

        //public async Task<Actor> GetById(int id)
        //{
        //    var result = await context.Actors.FirstOrDefaultAsync(a => a.id == id);
        //    return result;
        //}

        //public void Update(int id, Actor newActor)
        //{
        //    var existingActor = context.Actors.FirstOrDefault(a => a.id == id);
        //    if (existingActor != null)
        //    {
        //        existingActor.FullName = newActor.FullName;
        //        existingActor.Bio = newActor.Bio;
        //        existingActor.ProfilePicture = newActor.ProfilePicture;
        //    }
        //}
        ////public async Task SaveAsync()
        ////{
        ////    await context.SaveChangesAsync();
        ////}
        //public async Task Save()
        //{
        //   await context.SaveChangesAsync();
        //}
    }
}
