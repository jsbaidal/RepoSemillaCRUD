using Microsoft.EntityFrameworkCore;
using PersonasAPI.Data;
using PersonasAPI.Models;

namespace PersonasAPI.Repositorios;

public static class RepositorioPersonas
{
    public static Task<List<Persona>> ObtenerTodasAsync(PersonasDbContext db) =>
        db.Personas.AsNoTracking().ToListAsync();

    public static Task<Persona?> ObtenerPorIdAsync(PersonasDbContext db, int id) =>
        db.Personas.FindAsync(id).AsTask();

    public static async Task<Persona> CrearAsync(PersonasDbContext db, CamposPersona datos)
    {
        var persona = new Persona { Nombre = datos.Nombre, Correo = datos.Correo, Telefono = datos.Telefono };
        db.Personas.Add(persona);
        await db.SaveChangesAsync();
        return persona;
    }

    public static async Task<Persona?> ActualizarAsync(PersonasDbContext db, int id, CamposPersona datos)
    {
        var persona = await db.Personas.FindAsync(id);
        if (persona is null) return null;

        persona.Nombre = datos.Nombre;
        persona.Correo = datos.Correo;
        persona.Telefono = datos.Telefono;

        await db.SaveChangesAsync();
        return persona;
    }

    public static async Task<bool> EliminarAsync(PersonasDbContext db, int id)
    {
        var persona = await db.Personas.FindAsync(id);
        if (persona is null) return false;

        db.Personas.Remove(persona);
        await db.SaveChangesAsync();
        return true;
    }
}
