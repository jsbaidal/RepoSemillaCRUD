using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;

//Configs
var builder = WebApplication.CreateBuilder(args);
var frontendOrigin = builder.Configuration["FrontendOrigin"];
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.WithOrigins(frontendOrigin).AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddDbContext<PersonasDbContext>(options => options.UseSqlite("Data Source=personas.db"));
builder.Services.AddControllers();
builder.Services.AddProblemDetails();

var app = builder.Build();

//Middleware
app.UseExceptionHandler();
app.UseCors();

//Routes
app.MapControllers();

app.Run();
