using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public interface IActorsService
    {
        Task<IEnumerable<Actor>> GetAll();
        Actor GetById(int id);
        void Add(Actor actor);
        void Update(int id, Actor newActor);
        void Delete(int id);
    }
}
