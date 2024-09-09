using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Enum;

namespace Shared.Entities;

public class Blog
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public string Author { get; set; }
   
    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }

    #region Navigation Properties

    public CategoryEnum? FkCategory { get; set; }
    public Category Category { get; set; }

    #endregion
}