﻿namespace Courses.Common.DTOs;

public record CourseDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}
