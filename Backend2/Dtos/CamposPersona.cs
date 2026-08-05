using System.ComponentModel.DataAnnotations;

namespace Backend2.Dtos;

public class CamposPersona
{
    [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
    [MaxLength(3, ErrorMessage = "El tipo de identificación no puede superar los 3 caracteres")]
    public string Tipoidentificacion { get; }

    [Required(ErrorMessage = "El número de identificación es obligatorio")]
    [MaxLength(14, ErrorMessage = "El número de identificación no puede superar los 14 caracteres")]
    public string Numeroidentificacion { get; }

    [Required(ErrorMessage = "Los nombres son obligatorios")]
    [MaxLength(50, ErrorMessage = "Los nombres no pueden superar los 50 caracteres")]
    public string Nombres { get; }

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [MaxLength(50, ErrorMessage = "Los apellidos no pueden superar los 50 caracteres")]
    public string Apellidos { get; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    [MaxLength(80, ErrorMessage = "El correo no puede superar los 80 caracteres")]
    public string Email { get; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 dígitos")]
    public string Telefono { get; }

    public CamposPersona(string tipoidentificacion, string numeroidentificacion, string nombres, string apellidos, string email, string telefono)
    {
        Tipoidentificacion = tipoidentificacion?.Trim() ?? string.Empty;
        Numeroidentificacion = numeroidentificacion?.Trim() ?? string.Empty;
        Nombres = nombres?.Trim() ?? string.Empty;
        Apellidos = apellidos?.Trim() ?? string.Empty;
        Email = email?.Trim() ?? string.Empty;
        Telefono = telefono?.Trim() ?? string.Empty;
    }

    // Único lugar donde se mapean estos campos hacia la entidad — lo usan CrearAsync y ActualizarAsync
    // para no repetir el mismo bloque de asignaciones en los dos lados.
    public void AplicarA(Persona persona)
    {
        persona.Tipoidentificacion = Tipoidentificacion;
        persona.Numeroidentificacion = Numeroidentificacion;
        persona.Nombres = Nombres;
        persona.Apellidos = Apellidos;
        persona.Email = Email;
        persona.Telefonocontacto = Telefono;
    }
}
