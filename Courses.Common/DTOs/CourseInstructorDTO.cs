namespace Courses.Common.DTOs;

public record CourseInstructorDTO
{
    public int CourseId { get; set; } = default;
    public int InstructorId { get; set; } = default;

    public CourseInstructorDTO(int courseId, int instructorId) => (CourseId, InstructorId) = (courseId, instructorId);

}
