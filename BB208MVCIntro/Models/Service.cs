using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BB208MVCIntro.Models
{
    public class Service
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

    }
}
