using Car_Agency.Data;
using Car_Agency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Car_Agency.Controllers
{
    public class BrandsController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public BrandsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.Brands.ToList());
            }

            else
            {
                return View("Index", _context.Brands.Where(b => b.BrandName.Contains(search)).ToList());

            }
        }

        public IActionResult GetDetailsView(int id)
        {
            Brand brand = _context.Brands.Include(b => b.Cars).FirstOrDefault(d => d.Id == id);
            ViewBag.CurrentBran = brand;

            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                return View("Details", brand);
            }
        }

        public IActionResult GetAddView() 
        {
            return View("AddBrand");
        }
        [HttpPost]
        public IActionResult AddNew(Brand brand, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    brand.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                else
                {
                    brand.ImagePath = "\\images\\No_Image.png";
                }
                _context.Brands.Add(brand);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }

            else
            {
                return View("AddBrand", brand);
            }
        }

        public IActionResult GetEditView(int id)
        {
            Brand brand = _context.Brands.FirstOrDefault(d => d.Id == id);

            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit", brand);
            }
        }
        [HttpPost]
        public IActionResult EditCurrent(Brand brand, IFormFile? imageFormFile)
        {
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (brand.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _webHostEnvironment.WebRootPath + brand.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }

                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    brand.ImagePath = imgPath;
                    string imgFullPath = _webHostEnvironment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Dispose();
                }

                _context.Brands.Update(brand);
                _context.SaveChanges();

                return RedirectToAction("GetIndexView");
            }

            else
            {
                return View("Edit", brand);
            }
        }

        public IActionResult GetDeleteView(int id)
        {
            Brand brand = _context.Brands.Include(b => b.Cars).FirstOrDefault(d => d.Id == id);
            ViewBag.CurrentBran = brand;


            if (brand == null)
            {
                return NotFound();
            }
            else
            {
                return View("Delete", brand);
            }
        }
        [HttpPost]
        public IActionResult DeleteCurrent(int id) 
        {
            Brand brand = _context.Brands.Find(id);

            if (brand != null && brand.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _webHostEnvironment.WebRootPath + brand.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");

        }
    }
}
