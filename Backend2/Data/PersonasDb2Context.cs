using Microsoft.EntityFrameworkCore;
using Backend2.Models;

namespace Backend2.Data;

public class PersonasDb2Context : DbContext
{
    public PersonasDb2Context(DbContextOptions<PersonasDb2Context> options) : base(options) { }

    public DbSet<Persona> Personas => Set<Persona>();
}
