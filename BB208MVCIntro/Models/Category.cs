namespace BB208MVCIntro.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
