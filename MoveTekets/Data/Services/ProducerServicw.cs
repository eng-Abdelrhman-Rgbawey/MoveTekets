using MoveTekets.Data.Base;
using WebApplication1.Data;
using WebApplication1.Models;

namespace MoveTekets.Data.Services
{
    public class ProducerServicw : EntityBaseRepository<Producer>, IproducerService
    {
        public ProducerServicw(AppDbContext _context) : base(_context)
        {
        }
    }
}
