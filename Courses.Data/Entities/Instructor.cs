using Courses.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Courses.Data.Entities;

public class Instructor : IEntity
{
    public int Id { get; set; }
    [MaxLength(80), Required]
    public string? Name { get; set; }
    [MaxLength(1024)]
    public string? Description { get; set; }
    [MaxLength(1024)]
    public string? Thumbnail { get; set; }

    public virtual ICollection<Course>? Courses { get; set; }
}

