using Courses.Data.Interfaces;

namespace Courses.Data.Entities;

// Only to show how to manually create a many-to-many connection entity,
// which is genreated automatically if left out of the context.
public class CourseInstructor : IReferenceEntity
{
    public int CourseId { get; set; }
    public int InstructorId { get; set; }
    public virtual Course? Course { get; set; }
    public virtual Instructor? Instructor { get; set; }
}
