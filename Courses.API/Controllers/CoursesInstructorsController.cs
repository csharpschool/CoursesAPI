using Courses.API.Extensions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesInstructorsController : ControllerBase
    {
        private readonly IDbService _db;
        public CoursesInstructorsController(IDbService db) => _db = db;

        [HttpPost]
        public async Task<IResult> Post([FromBody] CourseInstructorDTO courseInstructor) =>
            await _db.HttpAddAsync<CourseInstructor, CourseInstructorDTO>(courseInstructor);
        
        [HttpDelete]
        public async Task<IResult> Delete(CourseInstructorDTO dto) =>
            await _db.HttpDeleteAsync<CourseInstructor, CourseInstructorDTO>(dto);
    }
}
