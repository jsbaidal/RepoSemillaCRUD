using System.ComponentModel.DataAnnotations;

namespace PersonasAPI.Models;

public record CamposPersona(
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    string Nombre,

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    [MaxLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres")]
    string Correo,

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
    [MaxLength(10, ErrorMessage = "El teléfono no puede superar los 10 caracteres")]
    string Telefono
);
