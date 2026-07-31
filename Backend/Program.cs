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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Middleware
app.UseExceptionHandler();
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Routes
app.MapControllers();

app.Run();
