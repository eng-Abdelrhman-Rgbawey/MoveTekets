using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public class CinemaService : EntityBaseRepository<Cinema>, IcinemaServices
    {
        public CinemaService(AppDbContext _context) : base(_context)
        {
        }
    }
}
