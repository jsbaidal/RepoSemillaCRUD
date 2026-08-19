import api from "../../lib/axios";

export async function listarPaises() {
  const respuesta = await api.get("/ubicaciones/paises");
  return respuesta.data;
}

export async function listarProvincias(paisId) {
  const respuesta = await api.get("/ubicaciones/provincias", {
    params: { paisId },
  });

  return respuesta.data;
}

export async function listarCantones(paisId, provinciaId) {
  const respuesta = await api.get("/ubicaciones/cantones", {
    params: { paisId, provinciaId },
  });

  return respuesta.data;
}
