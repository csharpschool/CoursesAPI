using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Courses.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly IDbService _db;
        public VideosController(IDbService db) => _db = db;
        
        // GET: api/<VideosController>
        [HttpGet]
        public async Task<IResult> Get() =>
            await _db.HttpGetAsync<Video, VideoDTO>();

        // GET api/<VideosController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id) => 
            await _db.HttpSingleAsync<Video, VideoDTO>(id);

        // POST api/<VideosController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] VideoDTO video) =>
            await _db.HttpPostAsync<Video, VideoDTO>(video);

        // PUT api/<VideosController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] VideoDTO video) =>
            await _db.HttpPutAsync<Video, VideoDTO>(id, video);

        // DELETE api/<VideosController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id) =>
            await _db.HttpDeleteAsync<Video>(id);
    }
}
