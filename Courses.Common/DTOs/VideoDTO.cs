namespace Courses.Common.DTOs;

public record VideoDTO
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
    public string? Url { get; set; }
}
