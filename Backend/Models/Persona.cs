using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models;

[Table("TBL_PERSONA",Schema ="ESPOL")]
public class Persona
{
    
    [Key]
    [Column("IDPERSONA")]
    public int Id { get; set; }

    [Column("TIPOIDENTIFICACION", TypeName = "character(3)")]
    public string TipoIdentificacion { get; set; } = "CED";

    [Column("NUMEROIDENTIFICACION")]
    public string NumeroIdentificacion { get; set; } = string.Empty;

    [Column("NOMBRES")]
    public string? Nombres { get; set; }

    [Column("APELLIDOS")]
    public string? Apellidos { get; set; }

    [Column("EMAIL")]
    public string? Email { get; set; }

    [Column("TELEFONOCONTACTO")]
    public string? Telefono { get; set; }
}
