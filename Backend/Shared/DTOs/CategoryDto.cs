namespace WebBlogAPI.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ImageId { get; set; }
        public string ImageData { get; set; } // New property
    }
}
