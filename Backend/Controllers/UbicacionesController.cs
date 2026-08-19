using Backend2.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers;

[ApiController]
[Route("ubicaciones")]
public class UbicacionesController : ControllerBase
{
    private readonly RepositorioUbicaciones _repositorio;

    public UbicacionesController(RepositorioUbicaciones repositorio)
    {
        _repositorio = repositorio;
    }

    // Catálogos dependientes: país → provincia → cantón

    [HttpGet("paises")]
    public async Task<IActionResult> ObtenerPaises()
    {
        var paises = await _repositorio.ObtenerPaisesAsync();

        return Ok(paises);
    }

    [HttpGet("provincias")]
    public async Task<IActionResult> ObtenerProvincias(short paisId)
    {
        var provincias = await _repositorio.ObtenerProvinciasAsync(paisId);

        return Ok(provincias);
    }

    [HttpGet("cantones")]
    public async Task<IActionResult> ObtenerCantones(
        short paisId,
        short provinciaId)
    {
        var cantones = await _repositorio.ObtenerCantonesAsync(
            paisId,
            provinciaId);

        return Ok(cantones);
    }
}
