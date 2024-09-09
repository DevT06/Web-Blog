using Shared.Enum;

namespace Shared.Entities;

public class Category
{
    public CategoryEnum Id { get; set; }

    public string Name { get; set; }

    #region Navigation Properties

    public ICollection<Blog> Blogs { get; set; }

    #endregion

    public Category()
    {
        Blogs = new List<Blog>();
    }
}