using Backend2.Data;
using Backend2.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Repositorios;

public class RepositorioPersonas
{
    private readonly PersonasDbContext _db;

    public RepositorioPersonas(PersonasDbContext db)
    {
        _db = db;
    }

    // Consultas

    public async Task<(List<Persona> Personas, int Total)> ObtenerPaginaAsync(
        int pagina,
        int tamanoPagina)
    {
        var total = await _db.Personas
            .CountAsync(persona => persona.Estado == "A");

        var desde = (pagina - 1) * tamanoPagina + 1;
        var hasta = pagina * tamanoPagina;

        var personas = await _db.Personas
            .FromSqlInterpolated($@"
                SELECT IDPERSONA, TIPOIDENTIFICACION, NUMEROIDENTIFICACION, NOMBRES, APELLIDOS, EMAIL, TELEFONOCONTACTO, ESTADO
                FROM (
                    SELECT t.*, ROW_NUMBER() OVER (ORDER BY t.IDPERSONA DESC) AS RN
                    FROM ESPOL.TBL_PERSONA t
                    WHERE t.ESTADO = 'A'
                ) AS Paginado
                WHERE RN BETWEEN {desde} AND {hasta}
                ORDER BY IDPERSONA DESC")
            .AsNoTracking()
            .ToListAsync();

        return (personas, total);
    }

    public async Task<Persona?> ObtenerPorIdAsync(int id)
    {
        return await _db.Personas
            .AsNoTracking()
            .FirstOrDefaultAsync(persona =>
                persona.Id == id && persona.Estado == "A");
    }

    public async Task<LugarDomicilio?> ObtenerDomicilioAsync(int personaId)
    {
        return await _db.LugaresDomicilio
            .AsNoTracking()
            .FirstOrDefaultAsync(domicilio =>
                domicilio.PersonaId == personaId && domicilio.Estado == "A");
    }

    // Validaciones

    public async Task<bool> IdentificacionExisteAsync(
        string numeroIdentificacion,
        int? personaIdExcluir = null)
    {
        return await _db.Personas
            .AsNoTracking()
            .AnyAsync(persona =>
                persona.NumeroIdentificacion == numeroIdentificacion &&
                (!personaIdExcluir.HasValue ||
                 persona.Id != personaIdExcluir.Value));
    }

    public async Task<bool> UbicacionValidaAsync(
        short paisId,
        short provinciaId,
        int cantonId)
    {
        var paisValido = await _db.Paises
            .AnyAsync(pais => pais.Id == paisId && pais.Estado == "A");

        if (!paisValido) return false;

        var provinciaValida = await _db.Provincias
            .AnyAsync(provincia =>
                provincia.Id == provinciaId &&
                provincia.PaisId == paisId &&
                provincia.Estado == "A");

        if (!provinciaValida) return false;

        return await _db.Cantones
            .AnyAsync(canton =>
                canton.Id == cantonId &&
                canton.ProvinciaId == provinciaId &&
                canton.PaisId == paisId &&
                canton.Estado == "A");
    }

    // Operaciones de escritura

    public async Task<Persona?> CrearAsync(
        Persona persona,
        LugarDomicilio domicilio)
    {
        var ubicacionValida = await UbicacionValidaAsync(
            domicilio.PaisId,
            domicilio.ProvinciaId!.Value,
            domicilio.CantonId!.Value);

        if (!ubicacionValida)
        {
            return null;
        }

        _db.Personas.Add(persona);
        _db.LugaresDomicilio.Add(domicilio);

        await _db.SaveChangesAsync();

        return persona;
    }

    public async Task<Persona?> ActualizarAsync(
        int id,
        Persona datosPersona,
        LugarDomicilio datosDomicilio)
    {
        var persona = await _db.Personas
            .FirstOrDefaultAsync(persona =>
                persona.Id == id && persona.Estado == "A");
        if (persona is null) return null;

        var domicilio = await _db.LugaresDomicilio
            .FirstOrDefaultAsync(domicilio =>
                domicilio.PersonaId == id && domicilio.Estado == "A");
        if (domicilio is null) return null;

        persona.TipoIdentificacion = datosPersona.TipoIdentificacion;
        persona.NumeroIdentificacion = datosPersona.NumeroIdentificacion;
        persona.Nombres = datosPersona.Nombres;
        persona.Apellidos = datosPersona.Apellidos;
        persona.Email = datosPersona.Email;
        persona.Telefono = datosPersona.Telefono;

        domicilio.PaisId = datosDomicilio.PaisId;
        domicilio.ProvinciaId = datosDomicilio.ProvinciaId;
        domicilio.CantonId = datosDomicilio.CantonId;
        domicilio.Direccion = datosDomicilio.Direccion;

        await _db.SaveChangesAsync();
        return persona;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var persona = await _db.Personas
            .FirstOrDefaultAsync(persona =>
                persona.Id == id && persona.Estado == "A");
        if (persona is null) return false;

        persona.Estado = "I";
        await _db.SaveChangesAsync();
        return true;
    }
}
