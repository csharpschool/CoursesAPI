using Courses.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Courses.Data.Entities;

public class Video : IEntity
{
    public int Id { get; set; }
    [MaxLength(80), Required]
    public string? Title { get; set; }
    [MaxLength(1024)]
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    [MaxLength(1024)]
    public string? Url { get; set; }

    public int CourseId { get; set; }
    public virtual Course? Course { get; set; }
}

