using Backend2.Data;
using Backend2.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Backend2.Repositorios;

public class RepositorioUbicaciones
{
    private readonly PersonasDbContext _db;

    public RepositorioUbicaciones(PersonasDbContext db)
    {
        _db = db;
    }

    public async Task<List<OpcionUbicacion>> ObtenerPaisesAsync()
    {
        return await _db.Paises
            .AsNoTracking()
            .Where(pais => pais.Estado == "A")
            .OrderBy(pais => pais.Nombre)
            .Select(pais => new OpcionUbicacion
            {
                Id = pais.Id,
                Nombre = pais.Nombre ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task<List<OpcionUbicacion>> ObtenerProvinciasAsync(short paisId)
    {
        return await _db.Provincias
            .AsNoTracking()
            .Where(provincia =>
                provincia.PaisId == paisId &&
                provincia.Estado == "A")
            .OrderBy(provincia => provincia.Nombre)
            .Select(provincia => new OpcionUbicacion
            {
                Id = provincia.Id,
                Nombre = provincia.Nombre ?? string.Empty
            })
            .ToListAsync();
    }

    public async Task<List<OpcionUbicacion>> ObtenerCantonesAsync(
        short paisId,
        short provinciaId)
    {
        return await _db.Cantones
            .AsNoTracking()
            .Where(canton =>
                canton.PaisId == paisId &&
                canton.ProvinciaId == provinciaId &&
                canton.Estado == "A")
            .OrderBy(canton => canton.Nombre)
            .Select(canton => new OpcionUbicacion
            {
                Id = canton.Id,
                Nombre = canton.Nombre ?? string.Empty
            })
            .ToListAsync();
    }
}
