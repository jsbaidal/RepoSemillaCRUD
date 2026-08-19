using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models;

[Table("TBL_PAIS", Schema = "ESPOL")]
public class Pais
{
    [Key]
    [Column("IDPAIS")]
    public short Id { get; set; }

    [Column("NOMBRE")]
    public string? Nombre { get; set; }

    [Column("ESTADO", TypeName = "character(1)")]
    public string? Estado { get; set; }
}
