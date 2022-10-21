using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IDbService _db;
        public CoursesController(IDbService db) => _db = db;

        // GET: api/<CoursesController>
        [HttpGet]
        public async Task<IResult> Get() =>
            await _db.HttpGetAsync<Course, CourseDTO>();

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id) => 
            await _db.HttpSingleAsync<Course, CourseDTO>(id);

        // POST api/<CoursesController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] CourseDTO course) =>
            await _db.HttpPostAsync<Course, CourseDTO>(course);

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] CourseDTO course) =>
            await _db.HttpPutAsync<Course, CourseDTO>(id, course);

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id) =>
            await _db.HttpDeleteAsync<Course>(id);
    }
}
