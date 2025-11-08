using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveTekets.Data.Services;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ImoveServices service;
        public MoviesController(ImoveServices _service)
        {
            service = _service;
        }
        public async Task<IActionResult> index()
        {
            //var data = await context.Movies
            //    .Include(m => m.Cinema)
            //    .OrderBy(m => m.Name)
            //    .ToListAsync();
            var data = await service.GetAll();
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var movie = await service.GetMoveByWithIncludes(id);
            if(movie == null) return View("NotFound");
            return View(movie);
        }
    }
}
