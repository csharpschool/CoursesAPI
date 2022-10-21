using Courses.Data.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly IDbService _db;
        public InstructorsController(IDbService db) => _db = db;

        // GET: api/<InstructorsController>
        [HttpGet]
        public async Task<IResult> Get() =>
            await _db.HttpGetAsync<Instructor, InstructorDTO>();

        // GET api/<InstructorsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id) => 
            await _db.HttpSingleAsync<Instructor, InstructorDTO>(id);

        // POST api/<InstructorsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] InstructorDTO instructor) =>
            await _db.HttpPostAsync<Instructor, InstructorDTO>(instructor);

        // PUT api/<InstructorsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] InstructorDTO instructor) =>
            await _db.HttpPutAsync<Instructor, InstructorDTO>(id, instructor);

        // DELETE api/<InstructorsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id) =>
            await _db.HttpDeleteAsync<Instructor>(id);
    }
}
