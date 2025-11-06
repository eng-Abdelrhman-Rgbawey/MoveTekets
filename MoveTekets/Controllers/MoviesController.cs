using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly AppDbContext context;
        public MoviesController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> index()
        {
            var data = await context.Movies
                .Include(m => m.Cinema)
                .OrderBy(m => m.Name)
                .ToListAsync();
            return View(data);
        }
    }
}
