using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models;

[Table("TBL_PROVINCIA", Schema = "ESPOL")]
public class Provincia
{
    [Key]
    [Column("IDPROVINCIA")]
    public short Id { get; set; }

    [Column("IDPAIS")]
    public short PaisId { get; set; }

    [Column("NOMBRE")]
    public string? Nombre { get; set; }

    [Column("ESTADO", TypeName = "character(1)")]
    public string? Estado { get; set; }
}
