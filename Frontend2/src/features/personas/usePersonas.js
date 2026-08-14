import { useState, useEffect } from "react";
import toast from "react-hot-toast";
import { listarPersonas, crearPersona, editarPersona, eliminarPersona } from "./personasApi";

const TAMANO_PAGINA = 12;

const mensajeDeError = (error, porDefecto) => {
  const datos = error.response?.data;
  const camposConError = Object.values(datos?.errors ?? {});

  if (camposConError.length === 1) return camposConError[0][0];
  if (camposConError.length > 1) return "Todos los campos son obligatorios";
  return datos?.detail || porDefecto;
};

export function usePersonas() {
  const [personas, setPersonas] = useState([]);
  const [total, setTotal] = useState(0);
  const [pagina, setPagina] = useState(1);
  const [cargando, setCargando] = useState(false);

  const cargar = async (paginaACargar) => {
    setCargando(true);
    try {
      const respuesta = await listarPersonas(paginaACargar, TAMANO_PAGINA);
      setPersonas(respuesta.personas);
      setTotal(respuesta.total);
      setPagina(respuesta.pagina);
    } catch {
      toast.error("No se pudieron cargar las personas");
    } finally {
      setCargando(false);
    }
  };

  useEffect(() => {
    cargar(1);
  }, []);

  const crear = async (datos) => {
    try {
      await crearPersona(datos);
      toast.success("Persona creada");
      await cargar(1);
    } catch (error) {
      toast.error(mensajeDeError(error, "No se pudo crear la persona"));
      throw error;
    }
  };

  const editar = async (id, datos) => {
    try {
      await editarPersona(id, datos);
      toast.success("Persona actualizada");
      await cargar(pagina);
    } catch (error) {
      toast.error(mensajeDeError(error, "No se pudo actualizar la persona"));
      throw error;
    }
  };

  const eliminar = async (id) => {
    try {
      await eliminarPersona(id);
      toast.success("Persona eliminada");
      await cargar(pagina);
    } catch (error) {
      toast.error(mensajeDeError(error, "No se pudo eliminar la persona"));
    }
  };

  const irASiguiente = () => cargar(pagina + 1);
  const irAAnterior = () => cargar(pagina - 1);

  return {
    personas,
    total,
    pagina,
    tamanoPagina: TAMANO_PAGINA,
    cargando,
    crear,
    editar,
    eliminar,
    irASiguiente,
    irAAnterior,
  };
}
