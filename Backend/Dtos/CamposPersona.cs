using System.ComponentModel.DataAnnotations;
using Backend2.Models;

namespace Backend2.Dtos;

public class CamposPersona
{
    [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
    [MaxLength(3, ErrorMessage = "El tipo de identificación no puede superar los 3 caracteres")]
    public string TipoIdentificacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de identificación es obligatorio")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de identificación debe tener exactamente 10 dígitos")]
    public string NumeroIdentificacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los nombres son obligatorios")]
    [MaxLength(20, ErrorMessage = "Los nombres no pueden superar los 20 caracteres")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los apellidos son obligatorios")]
    [MaxLength(20, ErrorMessage = "Los apellidos no pueden superar los 20 caracteres")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    [MaxLength(30, ErrorMessage = "El correo no puede superar los 30 caracteres")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener exactamente 10 dígitos")]
    public string Telefono { get; set; } = string.Empty;

    public void CopiarA(Persona persona)
    {
        persona.TipoIdentificacion = TipoIdentificacion;
        persona.NumeroIdentificacion = NumeroIdentificacion;
        persona.Nombres = Nombres;
        persona.Apellidos = Apellidos;
        persona.Email = Email;
        persona.Telefono = Telefono;
    }
}
