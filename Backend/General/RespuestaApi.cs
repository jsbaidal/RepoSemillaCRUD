namespace PersonasAPI.General;

public class RespuestaApi<T>
{
    public bool Exitoso { get; set; }
    public string? Mensaje { get; set; }
    public T? Data { get; set; }
    public IEnumerable<T>? ListaData { get; set; }
}
