using Microsoft.AspNetCore.Mvc;
using Backend2.Dtos;
using Backend2.Models;
using Backend2.Repositorios;

namespace Backend2.Controllers;

[ApiController]
[Route("personas")]
public class PersonasController : ControllerBase
{
    private readonly RepositorioPersonas _repositorio;

    public PersonasController(RepositorioPersonas repositorio)
    {
        _repositorio = repositorio;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerPersonas(int pagina = 1, int tamanoPagina = 20)
    {
        if (pagina < 1) pagina = 1;
        if (tamanoPagina is < 1 or > 200) tamanoPagina = 20;

        var (personas, total) = await _repositorio.ObtenerPaginaAsync(pagina, tamanoPagina);

        return Ok(new PersonasPaginadas
        {
            Total = total,
            Pagina = pagina,
            TamanoPagina = tamanoPagina,
            Personas = personas.Select(PersonaRespuesta.Mapear).ToList()
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPersonaPorId(int id)
    {
        var persona = await _repositorio.ObtenerPorIdAsync(id);
        if (persona is null) return NotFound();

        return Ok(PersonaRespuesta.Mapear(persona));
    }

    [HttpPost]
    public async Task<IActionResult> CrearPersona(CamposPersona datos)
    {
        var persona = new Persona();
        datos.CopiarA(persona);

        var creada = await _repositorio.CrearAsync(persona);

        return CreatedAtAction(nameof(ObtenerPersonaPorId), new { id = creada.Id }, PersonaRespuesta.Mapear(creada));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarPersona(int id, CamposPersona datos)
    {
        var persona = new Persona();
        datos.CopiarA(persona);

        var actualizada = await _repositorio.ActualizarAsync(id, persona);
        if (actualizada is null) return NotFound();

        return Ok(PersonaRespuesta.Mapear(actualizada));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarPersona(int id)
    {
        var eliminado = await _repositorio.EliminarAsync(id);
        if (!eliminado) return NotFound();

        return NoContent();
    }
}
