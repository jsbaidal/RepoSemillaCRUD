using Microsoft.AspNetCore.Mvc;
using PersonasAPI.Data;
using PersonasAPI.Models;
using PersonasAPI.Repositorios;

namespace PersonasAPI.Controllers;

[ApiController]
public class PersonasController(PersonasDbContext db) : ApiControllerBase
{
    [HttpGet("/")]
    public async Task<IActionResult> ObtenerPersonas()
    {
        var personas = await RepositorioPersonas.ObtenerTodasAsync(db);
        return RespuestaConLista(personas);
    }

    [HttpGet("/personas/{id}")]
    public async Task<IActionResult> ObtenerPersonaPorId(int id)
    {
        var persona = await RepositorioPersonas.ObtenerPorIdAsync(db, id);
        if (persona is null)
            return RespuestaNoEncontrado<Persona>($"No se encontro el usuario con el ID especificado ({id})");

        return RespuestaConDato(persona);
    }

    [HttpPost("/personas")]
    public async Task<IActionResult> CrearPersona(CamposPersona datos)
    {
        var persona = await RepositorioPersonas.CrearAsync(db, datos);
        return RespuestaCreado($"/personas/{persona.Id}", persona);
    }

    [HttpPut("/personas/{id}")]
    public async Task<IActionResult> ActualizarPersona(int id, CamposPersona datos)
    {
        var persona = await RepositorioPersonas.ActualizarAsync(db, id, datos);
        if (persona is null)
            return RespuestaNoEncontrado<Persona>($"No se encontro el usuario con el Id especificado ({id})");

        return RespuestaConDato(persona);
    }

    [HttpDelete("/personas/{id}")]
    public async Task<IActionResult> EliminarPersona(int id)
    {
        var eliminado = await RepositorioPersonas.EliminarAsync(db, id);
        if (!eliminado)
            return RespuestaNoEncontrado<Persona>($"No se encontro el usuario con el ID especificado ({id})");

        return RespuestaMensaje<Persona>($"Usuario con Id:{id}, eliminado correctamente");
    }
}
