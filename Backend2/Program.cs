using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Backend2.Data;
using Backend2.ManejoErrores;
using Backend2.Repositorios;

//Configs
var builder = WebApplication.CreateBuilder(args);
var frontendOrigin = builder.Configuration["FrontendOrigin"]
    ?? throw new InvalidOperationException("Falta configurar FrontendOrigin en appsettings");
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy => policy.WithOrigins(frontendOrigin).AllowAnyMethod().AllowAnyHeader()));

var connectionString = builder.Configuration.GetConnectionString("DBENT")
    ?? throw new InvalidOperationException("Falta configurar la connection string DBENT en appsettings");
builder.Services.AddDbContext<PersonasDb2Context>(options =>
    options.UseDb2(connectionString, db2 => db2.SetServerInfo(IBMDBServerType.LUW)));

builder.Services.AddScoped<IRepositorioPersonas, RepositorioPersonas>();

builder.Services.AddControllers();
builder.Services.AddExceptionHandler<Db2ExceptionHandler>();
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
