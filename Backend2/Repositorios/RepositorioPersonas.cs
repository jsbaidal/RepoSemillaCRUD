using Microsoft.EntityFrameworkCore;
using Backend2.Data;
using Backend2.Models;

namespace Backend2.Repositorios;

public class RepositorioPersonas(PersonasDb2Context db, ILogger<RepositorioPersonas> logger) : IRepositorioPersonas
{
    public async Task<(List<Persona> Personas, int Total)> ObtenerPaginadoAsync(int pagina, int tamanoPagina)
    {
        var total = await db.Personas.CountAsync();

        var desde = (pagina - 1) * tamanoPagina + 1;
        var hasta = pagina * tamanoPagina;

        // El proveedor de EF Core para DB2 no traduce Skip() a OFFSET: genera solo "FETCH FIRST N ROWS ONLY"
        // e ignora el Skip en silencio (page=2 devolvía lo mismo que page=1). ROW_NUMBER() es el workaround
        // nativo de DB2 para paginar por rango sin depender de esa traducción rota.
        var personas = await db.Personas
            .FromSqlInterpolated($@"
                SELECT IDPERSONA, TIPOIDENTIFICACION, NUMEROIDENTIFICACION, NOMBRES, APELLIDOS, EMAIL, TELEFONOCONTACTO
                FROM (
                    SELECT t.*, ROW_NUMBER() OVER (ORDER BY t.IDPERSONA) AS RN
                    FROM ESPOL.TBL_PERSONA t
                ) AS Paginado
                WHERE RN BETWEEN {desde} AND {hasta}
                ORDER BY IDPERSONA")
            .AsNoTracking()
            .ToListAsync();

        return (personas, total);
    }

    public Task<Persona?> ObtenerPorIdAsync(int id) =>
        db.Personas.FindAsync(id).AsTask();

    public async Task<Persona> CrearAsync(CamposPersona datos)
    {
        var persona = new Persona();
        datos.AplicarA(persona);

        db.Personas.Add(persona);
        await db.SaveChangesAsync();

        logger.LogInformation("Persona creada: Id={Id}, Numeroidentificacion={Numeroidentificacion}",
            persona.Id, persona.Numeroidentificacion);

        return persona;
    }

    public async Task<Persona?> ActualizarAsync(int id, CamposPersona datos)
    {
        var persona = await db.Personas.FindAsync(id);
        if (persona is null) return null;

        datos.AplicarA(persona);
        await db.SaveChangesAsync();

        logger.LogInformation("Persona actualizada: Id={Id}", id);

        return persona;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var persona = await db.Personas.FindAsync(id);
        if (persona is null) return false;

        db.Personas.Remove(persona);
        await db.SaveChangesAsync();

        logger.LogInformation("Persona eliminada: Id={Id}", id);

        return true;
    }
}
