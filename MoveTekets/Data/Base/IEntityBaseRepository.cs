using WebApplication1.Models;

namespace MoveTekets.Data.Base
{
    // we used IiEntityBase to make shure that T has an Id property
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(int id, T entity);
        Task Delete(int id);

        //public async Task SaveAsync();
        Task Save();
    }
}
