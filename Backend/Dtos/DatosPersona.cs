using System.ComponentModel.DataAnnotations;
using Backend2.Models;

namespace Backend2.Dtos;

public class DatosPersona
{
    [Required(ErrorMessage = "El tipo de identificación es obligatorio")]
    [RegularExpression(@"^(CED|RUC)$", ErrorMessage = "El tipo de identificación debe ser CED o RUC")]
    public string TipoIdentificacion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número de identificación es obligatorio")]
    [RegularExpression(@"^(\d{10}|\d{13})$", ErrorMessage = "El número de identificación debe tener 10 o 13 dígitos")]
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

    [Range(1, short.MaxValue, ErrorMessage = "Debe seleccionar un país")]
    public short PaisId { get; set; }

    [Range(1, short.MaxValue, ErrorMessage = "Debe seleccionar una provincia")]
    public short ProvinciaId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un cantón")]
    public int CantonId { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [MaxLength(200, ErrorMessage = "La dirección no puede superar los 200 caracteres")]
    public string Direccion { get; set; } = string.Empty;

    public Persona CrearPersona()
    {
        return new Persona
        {
            TipoIdentificacion = TipoIdentificacion,
            NumeroIdentificacion = NumeroIdentificacion,
            Nombres = Nombres,
            Apellidos = Apellidos,
            Email = Email,
            Telefono = Telefono
        };
    }

    public LugarDomicilio CrearDomicilio()
    {
        return new LugarDomicilio
        {
            PaisId = PaisId,
            ProvinciaId = ProvinciaId,
            CantonId = CantonId,
            Direccion = Direccion.Trim()
        };
    }
}
