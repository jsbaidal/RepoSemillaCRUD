namespace Backend2.Dtos;

// Lo que la API expone hacia afuera. Separado de Persona a propósito: Persona refleja
// los nombres de columna reales de TBL_PERSONA (ej. Telefonocontacto); acá el cliente
// ve nombres consistentes con lo que manda en el POST/PUT (ej. Telefono).
public class PersonaRespuesta
{
    public int Id { get; set; }
    public string Tipoidentificacion { get; set; } = string.Empty;
    public string Numeroidentificacion { get; set; } = string.Empty;
    public string? Nombres { get; set; }
    public string? Apellidos { get; set; }
    public string? Email { get; set; }
    public string? Telefono { get; set; }

    public static PersonaRespuesta DesdeEntidad(Persona persona) => new()
    {
        Id = persona.Id,
        Tipoidentificacion = persona.Tipoidentificacion,
        Numeroidentificacion = persona.Numeroidentificacion,
        Nombres = persona.Nombres,
        Apellidos = persona.Apellidos,
        Email = persona.Email,
        Telefono = persona.Telefonocontacto,
    };
}
