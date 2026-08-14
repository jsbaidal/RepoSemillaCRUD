using IBM.EntityFrameworkCore;
using Backend2.Data;
using Backend2.Repositorios;

var builder = WebApplication.CreateBuilder(args);

var frontendOrigin = builder.Configuration["FrontendOrigin"]
    ?? throw new InvalidOperationException("Falta configurar FrontendOrigin en appsettings");

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(frontendOrigin).AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("Db2")
    ?? throw new InvalidOperationException("Falta la cadena de conexión 'Db2' en appsettings");

builder.Services.AddDbContext<PersonasDbContext>(options =>
    options.UseDb2(connectionString, db2 => db2.SetServerInfo(IBMDBServerType.LUW)));

builder.Services.AddScoped<RepositorioPersonas>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
