using Courses.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialCoursesController : ControllerBase
    {
        private readonly IDbService _db;
        public InitialCoursesController(IDbService db) => _db = db;

        // GET: api/<SlaskController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAsync<Course, CourseDTO>());

        // GET api/<SlaskController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var result = await _db.SingleAsync<Course, CourseDTO>(e => e.Id.Equals(id));
            if (result is null) return Results.NotFound();
            return Results.Ok(result);
        }


        // POST api/<SlaskController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] CourseDTO dto) 
        {
            try
            {
                var entity = await _db.AddAsync<Course, CourseDTO>(dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Course).Name.ToLower();
                    return Results.Created($"/{node}s/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't add the {typeof(Course).Name} entity.\n{ex}.");
            }

            return Results.BadRequest($"Couldn't add the {typeof(Course).Name} entity.");
        }

        // PUT api/<SlaskController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] CourseDTO dto)
        {
            try
            {
                if (!await _db.AnyAsync<Course>(e => e.Id.Equals(id))) return Results.NotFound();

                _db.Update<Course, CourseDTO>(id, dto);
                if (await _db.SaveChangesAsync()) return Results.NoContent();

            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't update the {typeof(Course).Name} entity.\n{ex}.");
            }

            return Results.BadRequest($"Couldn't update the {typeof(Course).Name} entity.");
        }

        // DELETE api/<SlaskController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Course>(id)) return Results.NotFound();

                if (await _db.SaveChangesAsync()) return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Couldn't delete the {typeof(Course).Name} entity.\n{ex}.");
            }

            return Results.BadRequest($"Couldn't delete the {typeof(Course).Name} entity.");

        }
    }
}
