using Microsoft.AspNetCore.Mvc;
using PersonasAPI.General;

namespace PersonasAPI.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected IActionResult RespuestaConDato<T>(T data) =>
        Ok(new RespuestaApi<T> { Exitoso = true, Data = data });

    protected IActionResult RespuestaConLista<T>(IEnumerable<T> lista) =>
        Ok(new RespuestaApi<T> { Exitoso = true, ListaData = lista });

    protected IActionResult RespuestaCreado<T>(string ubicacion, T data) =>
        Created(ubicacion, new RespuestaApi<T> { Exitoso = true, Data = data });

    protected IActionResult RespuestaMensaje<T>(string mensaje) =>
        Ok(new RespuestaApi<T> { Exitoso = true, Mensaje = mensaje });

    protected IActionResult RespuestaNoEncontrado<T>(string mensaje) =>
        NotFound(new RespuestaApi<T> { Exitoso = false, Mensaje = mensaje });
}