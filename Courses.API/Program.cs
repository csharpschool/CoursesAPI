using System.Security.Policy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CourseContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("CourseConnection")));

ConfigureAutoMapper(builder.Services);
RegisterServices(builder.Services);

// Configure CORS
var _corsName = "CORS";
AddCors(builder.Services, _corsName);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors(_corsName);

app.UseAuthorization();

app.MapControllers();

app.Run();

// Configuration Methods
void ConfigureAutoMapper(IServiceCollection services)
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Course, CourseDTO>().ReverseMap();
        cfg.CreateMap<Instructor, InstructorDTO>().ReverseMap();
        cfg.CreateMap<Video, VideoDTO>().ReverseMap();
        cfg.CreateMap<CourseInstructor, CourseInstructorDTO>().ReverseMap();
    });
    var mapper = config.CreateMapper();
    services.AddSingleton(mapper);
}

void RegisterServices(IServiceCollection services)
{
    services.AddScoped<IDbService, DbService>();
}

void AddCors(IServiceCollection services, string corsName)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: corsName,
        policy =>
        {
            policy.WithOrigins("http://localhost:5000", "https://localhost:5001")
                  .AllowAnyMethod().AllowAnyHeader();
        });
    });
}
