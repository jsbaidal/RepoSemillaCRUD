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

    [HttpGet("paises")]
    public async Task<IActionResult> ObtenerPaises()
    {
        return Ok(await _repositorio.ObtenerPaisesAsync());
    }

    [HttpGet("provincias")]
    public async Task<IActionResult> ObtenerProvincias(short paisId)
    {
        return Ok(await _repositorio.ObtenerProvinciasAsync(paisId));
    }

    [HttpGet("cantones")]
    public async Task<IActionResult> ObtenerCantones(
        short paisId,
        short provinciaId)
    {
        return Ok(await _repositorio.ObtenerCantonesAsync(
            paisId,
            provinciaId));
    }
}
