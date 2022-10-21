using Courses.Data.Entities;
using Courses.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Courses.Data.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        [MaxLength(80), Required]
        public string? Title { get; set; }
        [MaxLength(1024)]
        public string? Description { get; set; }

        public virtual ICollection<Instructor>? Instructors { get; set; }
        public virtual ICollection<Video>? Videos { get; set; }
    }

}
