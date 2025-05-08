using Microsoft.AspNetCore.Mvc.Rendering;

namespace BB208MVCIntro.Areas.ViewModels.CategoryVMs
{
    public class BatchAssignVM
    {
        public int CategoryId { get; set; }
        public List<SelectListItem> AllServices { get; set; }
        public List<int> ServiceIds { get; set; }
    }
}
