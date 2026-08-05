namespace Backend2.Dtos;

public class PersonasPaginadasRespuesta
{
    public int Total { get; set; }
    public int Pagina { get; set; }
    public int TamanoPagina { get; set; }
    public List<PersonaRespuesta> Personas { get; set; } = [];
}
