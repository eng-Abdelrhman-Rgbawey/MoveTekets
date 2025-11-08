using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveTekets.Data.Services;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProducerController : Controller
    {
        private readonly IproducerService service;
        public ProducerController(IproducerService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await service.GetAll();
            return View(data);
        }

        // Create producer

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
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

                if (producer.ProfilePictureFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await producer.ProfilePictureFile.CopyToAsync(ms);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        ViewBag.ImagePreview = $"data:{producer.ProfilePictureFile.ContentType};base64,{base64}";
                    }
                }
                return View(producer);
            }

            if (producer.ProfilePictureFile == null)
            {
                ModelState.AddModelError("ProfilePictureFile", "Image is required");
                return View(producer);
            }

            string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Producers");
            if (!Directory.Exists(wwwRootPath))
                Directory.CreateDirectory(wwwRootPath);

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(producer.ProfilePictureFile.FileName);
            string filePath = Path.Combine(wwwRootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await producer.ProfilePictureFile.CopyToAsync(stream);
            }

            producer.ProfilePicture = "/images/Producers/" + fileName;

            await service.Add(producer);
            await service.Save();

            return RedirectToAction(nameof(Index));
        }

        // Get: Producer/Details
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await service.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        // Edit producer
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producer = await service.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (id == producer.id)
            {
                ModelState.Remove("ProfilePictureFile");

                if (!ModelState.IsValid)
                {
                    return View(producer);
                }

                var existingProducer = await service.GetById(id);
                if (existingProducer == null) return View("NotFound");
                if (producer.ProfilePictureFile != null)
                {
                    string wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Producers");
                    if (!Directory.Exists(wwwRootPath))
                        Directory.CreateDirectory(wwwRootPath);

                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(producer.ProfilePictureFile.FileName);
                    string filePath = Path.Combine(wwwRootPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await producer.ProfilePictureFile.CopyToAsync(stream);
                    }

                    if (!string.IsNullOrEmpty(existingProducer.ProfilePicture))
                    {
                        string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingProducer.ProfilePicture.TrimStart('/'));
                        if (System.IO.File.Exists(oldPath))
                            System.IO.File.Delete(oldPath);
                    }

                    existingProducer.ProfilePicture = "/images/Producers/" + fileName;
                }
                existingProducer.FullName = producer.FullName;
                existingProducer.Bio = producer.Bio;

                service.Update(id, existingProducer);
                await service.Save();
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }


        // Delete Producer
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producer = await service.GetById(id);
            if (producer == null) return View("NotFound");
            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var producer = await service.GetById(id);
            if (producer == null) return View("NotFound");
            if (!string.IsNullOrEmpty(producer.ProfilePicture))
            {
                string oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", producer.ProfilePicture.TrimStart('/'));
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }
            await service.Delete(id);
            await service.Save();
            return RedirectToAction("Index");
        }
    }
}
