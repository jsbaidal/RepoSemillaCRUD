using Microsoft.EntityFrameworkCore;
using Backend2.Models;

namespace Backend2.Data;

public class PersonasDbContext : DbContext
{
    //Constructor
    public PersonasDbContext(DbContextOptions<PersonasDbContext> options) : base(options) { }

    public DbSet<Persona> Personas => Set<Persona>();
}
