using BB208MVCIntro.DAL;
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BB208MVCIntro.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _webEnvironment;
        public ServiceController(AppDbContext appDbContext, IWebHostEnvironment webEnvironment)
        {
            _db = appDbContext;
            _webEnvironment = webEnvironment;
        }
        public IActionResult Index()
        {
            var services = _db.Services.Include(c => c.Category).ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View(service);
            }
            if(!service.ImageUpload.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageUpload", "File must be Image format");
                return View(service);
            }
            string filename = Guid.NewGuid() +  service.ImageUpload.FileName;
            string path = _webEnvironment.WebRootPath + @"\UploadImages\Services\";
            using (FileStream fileStream = new FileStream(path+filename, FileMode.Create))
            {
                service.ImageUpload.CopyTo(fileStream);
            }
            service.ImgUrl = filename;
            _db.Services.Add(service);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
