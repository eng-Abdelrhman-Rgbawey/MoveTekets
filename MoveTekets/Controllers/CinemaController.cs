using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CinemaController : Controller
    {
        private readonly AppDbContext context;
        public CinemaController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Cinemas.ToListAsync();
            return View(data);
        }
    }
}

// /images/Movies/move1.jpg
// /images/Movies/move2.jpg
// /images/Movies/move3.jpeg
// /images/Movies/move4.jpeg
// /images/Movies/move5.jpeg
// /images/Movies/move6.jpeg