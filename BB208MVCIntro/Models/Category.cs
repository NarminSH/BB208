using System.ComponentModel.DataAnnotations;

namespace BB208MVCIntro.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MinLength(3), MaxLength(25)]
        public string Title { get; set; }
        public ICollection<Service>? Services { get; set; }
    }
}
