using Microsoft.AspNetCore.Mvc;
using MoveTekets.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ActorsController : Controller
    {

        private readonly IActorsService service;
        public ActorsController(IActorsService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            return View(data); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

             service.Add(actor);   
            return RedirectToAction(nameof(Index));
        }
    }
}

