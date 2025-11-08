using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveTekets.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IcinemaServices service;
        public CinemaController(IcinemaServices _services)
        {
            service = _services;
        }
        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            return View(data);
        }

        // Create Cinema

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("ProfilePictureFile");

            if (!ModelState.IsValid)
            {
                //var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                //Console.WriteLine("==== MODELSTATE ERRORS ====");
                //foreach (var err in errors)
                //{
                //    Console.WriteLine(err);
                //}

                if (cinema.ProfilePictureFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await cinema.ProfilePictureFile.CopyToAsync(ms);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        ViewBag.ImagePreview = $"data:{cinema.ProfilePictureFile.ContentType};base64,{base64}";
                    }
                }
                return View(cinema);
            }

            if (cinema.ProfilePictureFile == null)
            {
                ModelState.AddModelError("ProfilePictureFile", "Image is required");
                return View(cinema);
            }

            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Cinema");
            if (!Directory.Exists(wwwRootPath))
                Directory.CreateDirectory(wwwRootPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cinema.ProfilePictureFile.FileName);
            string filePath = Path.Combine(wwwRootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cinema.ProfilePictureFile.CopyToAsync(stream);
            }

            cinema.Logo = "/images/Cinema/" + fileName;

            await service.Add(cinema);
            await service.Save();

            return RedirectToAction(nameof(Index));
        }

        // Get: Cinema/Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var cinema = await service.GetById(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }

        // Edit cinema
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await service.GetById(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (id == cinema.id)
            {
                ModelState.Remove("ProfilePictureFile");

                if (!ModelState.IsValid)
                {
                    return View(cinema);
                }

                var existingCinema = await service.GetById(id);
                if (existingCinema == null) return View("NotFound");
                if (cinema.ProfilePictureFile != null)
                {
                    string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Cinema");
                    if (!Directory.Exists(wwwRootPath))
                        Directory.CreateDirectory(wwwRootPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cinema.ProfilePictureFile.FileName);
                    string filePath = Path.Combine(wwwRootPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await cinema.ProfilePictureFile.CopyToAsync(stream);
                    }

                    if (!string.IsNullOrEmpty(existingCinema.Logo))
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingCinema.Logo.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    existingCinema.Logo = "/images/Cinema/" + fileName;
                }
                existingCinema.Name = cinema.Name;
                existingCinema.Description = cinema.Description;

                service.Update(id, existingCinema);
                await service.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }


        // Delete Cinema
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await service.GetById(id);
            if (cinema == null) return View("NotFound");
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var cinema = await service.GetById(id);
            if (cinema == null) return View("NotFound");
            if (!string.IsNullOrEmpty(cinema.Logo))
            {
                string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", cinema.Logo.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            await service.Delete(id);
            await service.Save();
            return RedirectToAction("Index");
        }
    }
}

// /images/Movies/move1.jpg
// /images/Movies/move2.jpg
// /images/Movies/move3.jpeg
// /images/Movies/move4.jpeg
// /images/Movies/move5.jpeg
// /images/Movies/move6.jpeg