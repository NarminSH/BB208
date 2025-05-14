namespace BB208MVCIntro.Areas.ViewModels.ServiceVMs
{
    public class UpdateServiceVM
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? ImageUpload { get; set; }
        public int CategoryId { get; set; }
        public string? ExistingImageUrl { get; set; }
    }
}
