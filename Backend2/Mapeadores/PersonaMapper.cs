using Backend2.Dtos;
using Backend2.Models;

namespace Backend2.Mapeadores;

// Único lugar que conoce tanto la entidad (Models) como los contratos de API (Dtos).
// Así ninguno de los dos necesita saber nada del otro.
public static class PersonaMapper
{
    public static void AplicarCampos(Persona persona, CamposPersona datos)
    {
        persona.Tipoidentificacion = datos.Tipoidentificacion;
        persona.Numeroidentificacion = datos.Numeroidentificacion;
        persona.Nombres = datos.Nombres;
        persona.Apellidos = datos.Apellidos;
        persona.Email = datos.Email;
        persona.Telefonocontacto = datos.Telefono;
    }

    public static PersonaRespuesta ARespuesta(Persona persona) => new()
    {
        Id = persona.Id,
        Tipoidentificacion = persona.Tipoidentificacion,
        Numeroidentificacion = persona.Numeroidentificacion,
        Nombres = persona.Nombres,
        Apellidos = persona.Apellidos,
        Email = persona.Email,
        Telefono = persona.Telefonocontacto,
    };
}
