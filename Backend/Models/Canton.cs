using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models;

[Table("TBL_CANTON", Schema = "ESPOL")]
public class Canton
{
    [Key]
    [Column("IDCANTON")]
    public int Id { get; set; }

    [Column("IDPROVINCIA")]
    public short ProvinciaId { get; set; }

    [Column("IDPAIS")]
    public short PaisId { get; set; }

    [Column("NOMBRE")]
    public string? Nombre { get; set; }

    [Column("ESTADO", TypeName = "character(1)")]
    public string? Estado { get; set; }
}
