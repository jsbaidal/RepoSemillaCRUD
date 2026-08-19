import api from "../../lib/axios";

export async function listarPersonas(pagina, tamanoPagina) {
  const respuesta = await api.get("/personas", {
    params: { pagina, tamanoPagina },
  });
  return respuesta.data;
}

export async function obtenerPersona(id) {
  const respuesta = await api.get(`/personas/${id}`);
  return respuesta.data;
}

export async function crearPersona(datos) {
  const respuesta = await api.post("/personas", datos);
  return respuesta.data;
}

export async function editarPersona(id, datos) {
  const respuesta = await api.put(`/personas/${id}`, datos);
  return respuesta.data;
}

export async function eliminarPersona(id) {
  await api.delete(`/personas/${id}`);
}
