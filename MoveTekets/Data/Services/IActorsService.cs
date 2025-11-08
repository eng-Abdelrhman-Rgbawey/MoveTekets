using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
        // we don't need to redeclare the methods here because they are inherited from IEntityBaseRepository<Actor>
        //Task<IEnumerable<Actor>> GetAll();
        //Task<Actor> GetById(int id);
        //Task Add(Actor actor);
        //void Update(int id, Actor newActor);
        //Task Delete(int id);
        ////public async Task SaveAsync();
        //Task Save();
    }
}
