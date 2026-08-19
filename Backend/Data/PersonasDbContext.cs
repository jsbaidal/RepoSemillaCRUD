using Microsoft.EntityFrameworkCore;
using Backend2.Models;

namespace Backend2.Data;

public class PersonasDbContext : DbContext
{
    public PersonasDbContext(DbContextOptions<PersonasDbContext> options)
        : base(options)
    {
    }

    public DbSet<Persona> Personas => Set<Persona>();
    public DbSet<Pais> Paises => Set<Pais>();
    public DbSet<Provincia> Provincias => Set<Provincia>();
    public DbSet<Canton> Cantones => Set<Canton>();
    public DbSet<LugarDomicilio> LugaresDomicilio => Set<LugarDomicilio>();
}
