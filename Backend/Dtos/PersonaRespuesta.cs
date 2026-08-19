using Backend2.Models;

namespace Backend2.Dtos;

public class PersonaRespuesta
{
    public int Id { get; set; }
    public string TipoIdentificacion { get; set; } = string.Empty;
    public string NumeroIdentificacion { get; set; } = string.Empty;
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }
    public short? PaisId { get; set; }
    public short? ProvinciaId { get; set; }
    public int? CantonId { get; set; }
    public string? Direccion { get; set; }

    public static PersonaRespuesta Mapear(
        Persona persona,
        LugarDomicilio? domicilio = null) => new()
    {
        Id = persona.Id,
        TipoIdentificacion = persona.TipoIdentificacion,
        NumeroIdentificacion = persona.NumeroIdentificacion,
        Nombres = persona.Nombres,
        Apellidos = persona.Apellidos,
        Email = persona.Email,
        Telefono = persona.Telefono,
        PaisId = domicilio?.PaisId,
        ProvinciaId = domicilio?.ProvinciaId,
        CantonId = domicilio?.CantonId,
        Direccion = domicilio?.Direccion,
    };
}
