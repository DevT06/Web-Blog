namespace Shared.DTOs;

public class BlogRequestDto
{
    public string Title { get; set; }

    public string? Text { get; set; }
    
    public string Author { get; set; }
    
    public DateTime LastChangedAt { get; set; }

    public int? CategoryId { get; set; }
}