using Microsoft.AspNetCore.Mvc;
using PersonasAPI.Data;
using PersonasAPI.Models;
using PersonasAPI.Repositorios;

namespace PersonasAPI.Controllers;

[ApiController]
public class PersonasController(PersonasDbContext db) : ControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> ObtenerPersonas()
    {
        var personas = await RepositorioPersonas.ObtenerTodasAsync(db);
        return Ok(personas);
    }

    [HttpGet("/personas/{id}")]
    public async Task<IActionResult> ObtenerPersonaPorId(int id)
    {
        var persona = await RepositorioPersonas.ObtenerPorIdAsync(db, id);
        if (persona is null)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el ID especificado ({id})");

        return Ok(persona);
    }

    [HttpPost("/personas")]
    public async Task<IActionResult> CrearPersona(CamposPersona datos)
    {
        var persona = await RepositorioPersonas.CrearAsync(db, datos);
        return Created($"/personas/{persona.Id}", persona);
    }

    [HttpPut("/personas/{id}")]
    public async Task<IActionResult> ActualizarPersona(int id, CamposPersona datos)
    {
        var persona = await RepositorioPersonas.ActualizarAsync(db, id, datos);
        if (persona is null)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el Id especificado ({id})");

        return Ok(persona);
    }

    [HttpDelete("/personas/{id}")]
    public async Task<IActionResult> EliminarPersona(int id)
    {
        var eliminado = await RepositorioPersonas.EliminarAsync(db, id);
        if (!eliminado)
            return Problem(statusCode: StatusCodes.Status404NotFound, title: "Recurso no encontrado",
                detail: $"No se encontro el usuario con el ID especificado ({id})");

        return NoContent();
    }
}
