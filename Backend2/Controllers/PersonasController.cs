using Microsoft.AspNetCore.Mvc;
using Backend2.Models;
using Backend2.Repositorios;

namespace Backend2.Controllers;

[ApiController]
public class PersonasController(IRepositorioPersonas repositorio) : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> ObtenerPersonas(int pagina = 1, int tamanoPagina = 20)
    {
        if (pagina < 1) pagina = 1;
        if (tamanoPagina is < 1 or > 200) tamanoPagina = 20;

        var (personas, total) = await repositorio.ObtenerPaginadoAsync(pagina, tamanoPagina);
        return Ok(new PersonasPaginadasRespuesta
        {
            Total = total,
            Pagina = pagina,
            TamanoPagina = tamanoPagina,
            Personas = personas.Select(PersonaRespuesta.DesdeEntidad).ToList()
        });
    }

    [HttpGet("/personas/{id}")]
    public async Task<IActionResult> ObtenerPersonaPorId(int id)
    {
        var persona = await repositorio.ObtenerPorIdAsync(id);
        if (persona is null)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el ID especificado ({id})");

        return Ok(PersonaRespuesta.DesdeEntidad(persona));
    }

    [HttpPost("/personas")]
    public async Task<IActionResult> CrearPersona(CamposPersona datos)
    {
        var persona = await repositorio.CrearAsync(datos);
        return Created($"/personas/{persona.Id}", PersonaRespuesta.DesdeEntidad(persona));
    }

    [HttpPut("/personas/{id}")]
    public async Task<IActionResult> ActualizarPersona(int id, CamposPersona datos)
    {
        var persona = await repositorio.ActualizarAsync(id, datos);
        if (persona is null)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el Id especificado ({id})");

        return Ok(PersonaRespuesta.DesdeEntidad(persona));
    }

    [HttpDelete("/personas/{id}")]
    public async Task<IActionResult> EliminarPersona(int id)
    {
        var eliminado = await repositorio.EliminarAsync(id);
        if (!eliminado)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el ID especificado ({id})");

        return NoContent();
    }
}
