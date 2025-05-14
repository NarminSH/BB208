using BB208MVCIntro.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BB208MVCIntro.Areas.ViewModels.ServiceVMs
{
    public class CreateServiceVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageUpload { get; set; }
        public int CategoryId { get; set; }
 
    }
}
