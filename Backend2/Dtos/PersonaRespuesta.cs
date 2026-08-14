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

    public static PersonaRespuesta Mapear(Persona persona) => new()
    {
        Id = persona.Id,
        TipoIdentificacion = persona.TipoIdentificacion,
        NumeroIdentificacion = persona.NumeroIdentificacion,
        Nombres = persona.Nombres,
        Apellidos = persona.Apellidos,
        Email = persona.Email,
        Telefono = persona.Telefono,
    };
}
