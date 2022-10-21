using Courses.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Courses.Data.Contexts;

public class CourseContext : DbContext
{
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Instructor> Instructors => Set<Instructor>();
    public DbSet<Video> Videos => Set<Video>();
    public DbSet<CourseInstructor> CourseInstructors => Set<CourseInstructor>();

    public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<CourseInstructor>().HasKey(ci => new { ci.CourseId, ci.InstructorId });
        SeedData(builder);
    }

    private void SeedData(ModelBuilder builder)
    {
        var instructors = new List<Instructor>
        {
            new Instructor {
                Id = 1,
                Name = "John Doe",
                Description = "John Doe's description.",
                Thumbnail = "/images/john.png"
            },
            new Instructor {
                Id = 2,
                Name = "Jane Doe",
                Description = "Jane Doe's description.",
                Thumbnail = "/images/jane.png"
            }
        };
        builder.Entity<Instructor>().HasData(instructors);

        var videos = new List<Video>
        {
            new Video {
                Id = 1,
                CourseId = 1,
                Title = "Video 1 Title",
                Description = "Video 1 description",
                Thumbnail = "Thumbnail 1 Url",
                Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
            },
            new Video {
                Id = 2,
                CourseId = 1,
                Title = "Video 2 Title",
                Description = "Video 2 description",
                Thumbnail = "Thumbnail 2 Url",
                Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
            },
            new Video {
                Id = 3,
                CourseId = 2,
                Title = "Video 3 Title",
                Description = "Video 3 description",
                Thumbnail = "Thumbnail 3 Url",
                Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
            },
            new Video {
                Id = 4,
                CourseId = 3,
                Title = "Video 4 Title",
                Description = "Video 4 description",
                Thumbnail = "Thumbnail 4 Url",
                Url = "https://www.youtube.com/embed/BJFyzpBcaCY"
            }
        };
        builder.Entity<Video>().HasData(videos);

        var courses = new List<Course>
        {
            new(){Id = 1, Title = "Course 1", Description = "Course 1 description." },
            new(){Id = 2, Title = "Course 2", Description = "Course 2 description." },
            new(){Id = 3, Title = "Course 3", Description = "Course 3 description." },
        };
        builder.Entity<Course>().HasData(courses);

        var courseInstructors = new List<CourseInstructor>
        {
            new(){ CourseId = 1, InstructorId = 1 },
            new(){ CourseId = 1, InstructorId = 2 },
            new(){ CourseId = 2, InstructorId = 2 },
            new(){ CourseId = 3, InstructorId = 1 }
        };
        builder.Entity<CourseInstructor>().HasData(courseInstructors);
    }
}
