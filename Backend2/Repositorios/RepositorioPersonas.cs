using Microsoft.EntityFrameworkCore;
using Backend2.Data;
using Backend2.Models;

namespace Backend2.Repositorios;

public class RepositorioPersonas
{
    private readonly PersonasDbContext _db;

    public RepositorioPersonas(PersonasDbContext db)
    {
        _db = db;
    }

        public async Task<(List<Persona> Personas, int Total)> ObtenerPaginaAsync(int pagina, int tamanoPagina)
    {
        var total = await _db.Personas.CountAsync();

        var desde = (pagina - 1) * tamanoPagina + 1;
        var hasta = pagina * tamanoPagina;

        var personas = await _db.Personas
            .FromSqlInterpolated($@"
                SELECT IDPERSONA, TIPOIDENTIFICACION, NUMEROIDENTIFICACION, NOMBRES, APELLIDOS, EMAIL, TELEFONOCONTACTO
                FROM (
                    SELECT t.*, ROW_NUMBER() OVER (ORDER BY t.IDPERSONA DESC) AS RN
                    FROM ESPOL.TBL_PERSONA t
                ) AS Paginado
                WHERE RN BETWEEN {desde} AND {hasta}
                ORDER BY IDPERSONA DESC")
            .AsNoTracking()
            .ToListAsync();

        return (personas, total);
    }

    public async Task<Persona?> ObtenerPorIdAsync(int id)
    {
        return await _db.Personas.FindAsync(id);
    }

    public async Task<Persona> CrearAsync(Persona persona)
    {
        _db.Personas.Add(persona);
        await _db.SaveChangesAsync();
        return persona;
    }

    public async Task<Persona?> ActualizarAsync(int id, Persona datos)
    {
        var persona = await _db.Personas.FindAsync(id);
        if (persona is null) return null;

        persona.TipoIdentificacion = datos.TipoIdentificacion;
        persona.NumeroIdentificacion = datos.NumeroIdentificacion;
        persona.Nombres = datos.Nombres;
        persona.Apellidos = datos.Apellidos;
        persona.Email = datos.Email;
        persona.Telefono = datos.Telefono;

        await _db.SaveChangesAsync();
        return persona;
    }

    // #5 - Eliminar una persona por su id. Devuelve true si se borró, false si no existía.
    public async Task<bool> EliminarAsync(int id)
    {
        var persona = await _db.Personas.FindAsync(id);
        if (persona is null) return false;

        _db.Personas.Remove(persona);
        await _db.SaveChangesAsync();
        return true;
    }
}
