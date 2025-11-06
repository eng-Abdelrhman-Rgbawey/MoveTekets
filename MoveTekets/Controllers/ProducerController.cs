using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class ProducerController : Controller
    {
        private readonly AppDbContext context;
        public ProducerController(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<IActionResult> Index()
        {
            var data = await context.Producers.ToListAsync();
            return View(data);
        }
    }
}
