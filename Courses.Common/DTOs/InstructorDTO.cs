namespace Courses.Common.DTOs;

public record InstructorDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Thumbnail { get; set; }
}