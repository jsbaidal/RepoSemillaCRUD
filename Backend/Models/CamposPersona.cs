using System.ComponentModel.DataAnnotations;

namespace PersonasAPI.Models;

public class CamposPersona
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres")]
    public string Nombre { get; }

    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
    [MaxLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres")]
    public string Correo { get; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
    [MaxLength(10, ErrorMessage = "El teléfono no puede superar los 10 caracteres")]
    
    public string Telefono { get; }

    public CamposPersona(string nombre, string correo, string telefono)
    {
        Nombre = nombre?.Trim() ?? string.Empty;
        Correo = correo?.Trim() ?? string.Empty;
        Telefono = telefono?.Trim() ?? string.Empty;
    }
}
