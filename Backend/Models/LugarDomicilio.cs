using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend2.Models;

[Table("TBL_LUGAR_DOMICILIO", Schema = "ESPOL")]
public class LugarDomicilio
{
    [Key]
    [Column("IDLUGARDOMICILIO")]
    public int Id { get; set; }

    [Column("IDCANTON")]
    public int? CantonId { get; set; }

    [Column("IDPROVINCIA")]
    public short? ProvinciaId { get; set; }

    [Column("IDPAIS")]
    public short PaisId { get; set; }

    [Column("DIRECCION")]
    [MaxLength(200)]
    public string? Direccion { get; set; }

    [Column("IDPERSONA")]
    public int PersonaId { get; set; }

    [Column("ESTADO", TypeName = "character(1)")]
    public string Estado { get; set; } = "A";

    [Column("TIPO", TypeName = "character(1)")]
    public string Tipo { get; set; } = "L";

    public Persona Persona { get; set; } = null!;
}
