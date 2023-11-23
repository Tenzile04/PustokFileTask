using Microsoft.AspNetCore.Mvc;
using PustokTask1.DAL;
using PustokTask1.Models;
using System.Runtime.CompilerServices;

namespace PustokTask1.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SlideController : Controller
    {

        private readonly AppDbContext _context;
        public SlideController(AppDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            string fileName = slider.ImageFile.FileName;
            if (slider.ImageFile.ContentType !="image/jpeg" && slider.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "can only upload .jpeg or .png");
            }

            if (slider.ImageFile.Length > 1048576)
            {
                ModelState.AddModelError("ImageFile", "File size must be lower than 1mb");
            }

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }
            fileName = Guid.NewGuid().ToString() + fileName;


            string path = "C:\\Users\\Tenzıle SSD\\Desktop\\TaskLessonPustok\\PustokTask1\\wwwroot\\uploads\\sliders\\"+fileName;
            
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slider.ImageFile.CopyTo(fileStream);
            }

            if (!ModelState.IsValid)
            {
                return View(slider);
            }
            slider.ImageUrl = slider.ImageFile.FileName;

            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
       
        public IActionResult Update(int id)
        {
            
            Slider wantedSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSlider == null) return NotFound();

            return View(wantedSlider);
        }

        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            string fileName = slider.ImageFile.FileName;
            if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "can only upload .jpeg or .png");
            }

            if (slider.ImageFile.Length > 1048576)
            {
                ModelState.AddModelError("ImageFile", "File size must be lower than 1mb");
            }

            if (fileName.Length > 64)
            {
                fileName = fileName.Substring(fileName.Length - 64, 64);
            }
            fileName = Guid.NewGuid().ToString() + fileName;
            string path = "C:\\Users\\Tenzıle SSD\\Desktop\\TaskLessonPustok\\PustokTask1\\wwwroot\\uploads\\sliders\\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                slider.ImageFile.CopyTo(fileStream);
            }


            if (!ModelState.IsValid) return View(slider);

            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSlider == null) return NotFound();

            //Internetde arasdirdim amma mentiqi tam aydin olmadi deye tam yaza bilmedim

            //if (!string.IsNullOrEmpty(existSlider.ImageUrl))
            //{
            //    string oldImagePath = "C:\\Users\\Tenzıle SSD\\Desktop\\TaskLessonPustok\\PustokTask1\\wwwroot\\uploads\\sliders\\" + existSlider.ImageUrl;
            //    if (System.IO.File.Exists(oldImagePath))
            //    {
            //        System.IO.File.Delete(oldImagePath);
            //    }
            //}



            existSlider.Title = slider.Title;
            existSlider.Description = slider.Description;
            existSlider.RedirectUrl = slider.RedirectUrl;        
            existSlider.ImageUrl = fileName;
            existSlider.RedirecText = slider.RedirecText;
            existSlider.ImageFile = slider.ImageFile;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Slider wantedSlider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            if (wantedSlider == null) return NotFound();

            return View(wantedSlider);
        }

        [HttpPost]
        public IActionResult Delete(Slider slider)
        {
            Slider existSlider = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);

            if (existSlider == null) return NotFound();


            _context.Sliders.Remove(existSlider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
