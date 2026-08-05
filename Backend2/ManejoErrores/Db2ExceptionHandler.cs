using IBM.Data.Db2;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend2.ManejoErrores;

public class Db2ExceptionHandler(ILogger<Db2ExceptionHandler> logger) : IExceptionHandler
{
    private const string SqlStateViolacionUnicidad = "23505";

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DbUpdateException { InnerException: DB2Exception db2Ex })
            return false;

        // DB2Exception.SqlState (la propiedad de nivel superior) no viene poblada en este driver;
        // el código real está dentro de la colección Errors. Se usa esa como fuente principal y
        // el top-level como respaldo por si algún día el driver empieza a completarla.
        var sqlState = db2Ex.Errors.Count > 0 ? db2Ex.Errors[0].SQLState : db2Ex.SqlState;

        var (status, title, detail) = sqlState == SqlStateViolacionUnicidad
            ? (StatusCodes.Status409Conflict, "Registro duplicado",
                "Ya existe una persona con ese número de identificación.")
            : (StatusCodes.Status400BadRequest, "No se pudo guardar la persona",
                "Los datos enviados no cumplen con una restricción de la base de datos.");

        logger.LogWarning(db2Ex, "Error de DB2 al guardar (SQLSTATE={SqlState}) -> HTTP {Status}", sqlState, status);

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = detail
        }, cancellationToken);

        return true;
    }
}
