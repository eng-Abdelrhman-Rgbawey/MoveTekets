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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(Actor actor)
        //{

        //    ModelState.Remove("ProfilePicture");

        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors);
        //        foreach (var error in errors)
        //        {
        //            Console.WriteLine(error.ErrorMessage);
        //        }
        //        return View(actor);
        //    }

        //    // Store the path of the image
        //    string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Actors");
        //    if (!Directory.Exists(wwwRootPath))
        //        Directory.CreateDirectory(wwwRootPath);

        //    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(actor.ProfilePictureFile.FileName);
        //    string filePath = Path.Combine(wwwRootPath, fileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await actor.ProfilePictureFile.CopyToAsync(stream);
        //    }

        //    actor.ProfilePicture = "/images/Actors/" + fileName;

        //    service.Add(actor);
        //    service.Save();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
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
                if (actor.ProfilePictureFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await actor.ProfilePictureFile.CopyToAsync(ms);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        ViewBag.ImagePreview = $"data:{actor.ProfilePictureFile.ContentType};base64,{base64}";
                    }
                }
                return View(actor);
            }

            if (actor.ProfilePictureFile == null)
            {
                ModelState.AddModelError("ProfilePictureFile", "Image is required");
                return View(actor);
            }

            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Actors");
            if (!Directory.Exists(wwwRootPath))
                Directory.CreateDirectory(wwwRootPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(actor.ProfilePictureFile.FileName);
            string filePath = Path.Combine(wwwRootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await actor.ProfilePictureFile.CopyToAsync(stream);
            }

            actor.ProfilePicture = "/images/Actors/" + fileName;

            await service.Add(actor);
            await service.Save();

            return RedirectToAction(nameof(Index));
        }

        // Get: Actors/Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var actor = await service.GetById(id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }

        // Edit Actor
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await service.GetById(id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (id == actor.id)
            {
                ModelState.Remove("ProfilePictureFile");

                if(!ModelState.IsValid)
                {
                    return View(actor);
                }

                var existingActor = await service.GetById(id);
                if (existingActor == null) return View("NotFound");
                if (actor.ProfilePictureFile != null)
                {
                    string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Actors");
                    if (!Directory.Exists(wwwRootPath))
                        Directory.CreateDirectory(wwwRootPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(actor.ProfilePictureFile.FileName);
                    string filePath = Path.Combine(wwwRootPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await actor.ProfilePictureFile.CopyToAsync(stream);
                    }

                    if (!string.IsNullOrEmpty(existingActor.ProfilePicture))
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingActor.ProfilePicture.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    existingActor.ProfilePicture = "/images/Actors/" + fileName;
                }
                existingActor.FullName = actor.FullName;
                existingActor.Bio = actor.Bio;

                service.Update(id, existingActor);
                await service.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }


        // Delete Actor
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await service.GetById(id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

                var actor = await service.GetById(id);
                if (actor == null) return View("NotFound");
                if (!string.IsNullOrEmpty(actor.ProfilePicture))
                {
                    string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", actor.ProfilePicture.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                await service.Delete(id);
                await service.Save();
            return RedirectToAction("Index");
        }

    }
}

