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
            Personas = personas
                .Select(persona => PersonaRespuesta.Mapear(persona))
                .ToList()
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPersonaPorId(int id)
    {
        var persona = await _repositorio.ObtenerPorIdAsync(id);
        if (persona is null) return NotFound();

        var domicilio = await _repositorio.ObtenerDomicilioAsync(id);

        return Ok(PersonaRespuesta.Mapear(persona, domicilio));
    }

    [HttpPost]
    public async Task<IActionResult> CrearPersona(DatosPersona datos)
    {
        if (await _repositorio.IdentificacionExisteAsync(
            datos.NumeroIdentificacion))
        {
            return Conflict(new
            {
                detail = "Ya existe una persona con esa cédula o RUC"
            });
        }

        var persona = datos.CrearPersona();
        var domicilio = datos.CrearDomicilio();

        domicilio.Persona = persona;

        var creada = await _repositorio.CrearAsync(persona, domicilio);

        if (creada is null)
        {
            return BadRequest(new
            {
                detail = "El país, la provincia y el cantón seleccionados no corresponden entre sí"
            });
        }

        return CreatedAtAction(nameof(ObtenerPersonaPorId), new { id = creada.Id }, PersonaRespuesta.Mapear(creada));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarPersona(int id, DatosPersona datos)
    {
        if (await _repositorio.IdentificacionExisteAsync(
            datos.NumeroIdentificacion,
            id))
        {
            return Conflict(new
            {
                detail = "Ya existe otra persona con esa cédula o RUC"
            });
        }

        if (!await _repositorio.UbicacionValidaAsync(
            datos.PaisId,
            datos.ProvinciaId,
            datos.CantonId))
        {
            return BadRequest(new
            {
                detail = "El país, la provincia y el cantón seleccionados no corresponden entre sí"
            });
        }

        var persona = datos.CrearPersona();
        var domicilio = datos.CrearDomicilio();

        var actualizada = await _repositorio.ActualizarAsync(
            id,
            persona,
            domicilio);
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
