using Microsoft.EntityFrameworkCore;
using PersonasAPI.Models;

namespace PersonasAPI.Data;

public class PersonasDbContext : DbContext
{
    public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options) { }

    public DbSet<Persona> Personas => Set<Persona>();
}
