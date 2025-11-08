using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public interface ImoveServices 
    {
        Task<IEnumerable<Move>> GetAll();
        Task<Move> GitById(int id);

        Task Add(Move entity);
        Task <Move> Update(int id, Move entity);
        Task Delete(int id);

        Task<IEnumerable<Cinema>> GetAllCinemas();
        Task<IEnumerable<Producer>> GetAllProducers();
        Task<IEnumerable<Actor>> GetAllActors();

        Task<Move> GetMoveByWithIncludes(int id);

        Task save();
    }
}
