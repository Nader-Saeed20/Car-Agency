using Car_Agency.Data;
using Car_Agency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Car_Agency.Controllers
{
    public class CarsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public CarsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.Cars.ToList());
            }

            else
            {
                return View("Index", _context.Cars.Where(c => c.CarName.Contains(search)).ToList());

            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Car car = _context.Cars.Include(c => c.Brand).FirstOrDefault(c => c.Id == id);
            ViewBag.CurrentCar = car;

            if (car == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", car);
            }
        }

        public IActionResult GetAddView()
        {
            SelectList brandSelectList = new SelectList(_context.Brands, "Id", "BrandName");
            ViewBag.AllBrands = _context.Brands.ToList();
            return View("AddCar");
        }
        [HttpPost]
        public IActionResult AddNew(Car car, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    car.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                else
                {
                    car.ImagePath = "\\images\\No_Image.png";
                }
                _context.Cars.Add(car);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

            else
            {
                ViewBag.AllBrands = _context.Brands.ToList();

                return View("AddCar", car);
            }
        }

        public IActionResult GetEditView(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            else
            {
                ViewBag.AllBrands = _context.Brands.ToList();

                return View("Edit", car);
            }
        }
        [HttpPost]
        public IActionResult EditCurrent(Car car, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (car.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + car.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    car.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                _context.Cars.Update(car);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }

            else
            {
                ViewBag.AllBrands = _context.Brands.ToList();

                return View("Edit", car);
            }
        }

        public IActionResult GetDeleteView(int id)
        {
            Car car = _context.Cars.Include(c => c.Brand).FirstOrDefault(c => c.Id == id);
            ViewBag.CurrentCar = car;


            if (car == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", car);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id)
        {
            Car car = _context.Cars.Find(id);

            if (car != null && car.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + car.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");

        }
    }
}
