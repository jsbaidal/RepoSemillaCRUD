using Backend2.Models;

namespace Backend2.Repositorios;

public interface IRepositorioPersonas
{
    Task<(List<Persona> Personas, int Total)> ObtenerPaginadoAsync(int pagina, int tamanoPagina);
    Task<Persona?> ObtenerPorIdAsync(int id);
    Task<Persona> CrearAsync(CamposPersona datos);
    Task<Persona?> ActualizarAsync(int id, CamposPersona datos);
    Task<bool> EliminarAsync(int id);
}
