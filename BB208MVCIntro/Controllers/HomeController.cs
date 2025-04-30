
using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BB208MVCIntro.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Service s1 = new Service()
            {
                Id = 1,
                Title = "Backend development",
                Description = "asdfghjklfjhgdsdfghjhdhsgfa",
                ImgUrl = "assets/img/services-01.jpg"
            };
            Service s2 = new Service()
            {
                Id = 2,
                Title = "Social media",
                Description = "asdfghjklfjhgdsdfghjhdhsgfa",
                ImgUrl = "assets/img/services-02.jpg"
            };
            Service s3 = new Service()
            {
                Id = 3,
                Title = "Marketing",
                Description = "asdfghjklfjhgdsdfghjhdhsgfa",
                ImgUrl = "assets/img/services-03.jpg"
            };
            List<Service> services = new List<Service>() { s1, s2, s3 };
            return View(services);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
