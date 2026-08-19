import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import {
  listarPaises,
  listarProvincias,
  listarCantones,
} from "./ubicacionesApi";

export function useUbicaciones(paisId, provinciaId) {
  // Opciones disponibles
  const [paises, setPaises] = useState([]);
  const [provincias, setProvincias] = useState([]);
  const [cantones, setCantones] = useState([]);

  // Estados de carga
  const [cargandoPaises, setCargandoPaises] = useState(false);
  const [cargandoProvincias, setCargandoProvincias] = useState(false);
  const [cargandoCantones, setCargandoCantones] = useState(false);

  // Carga inicial de países

  useEffect(() => {
    const cargarPaises = async () => {
      setCargandoPaises(true);

      try {
        setPaises(await listarPaises());
      } catch {
        toast.error("No se pudieron cargar los países");
      } finally {
        setCargandoPaises(false);
      }
    };

    cargarPaises();
  }, []);

  // Provincias dependientes del país

  useEffect(() => {
    setProvincias([]);
    setCantones([]);

    if (!paisId) return;

    const cargarProvincias = async () => {
      setCargandoProvincias(true);

      try {
        setProvincias(await listarProvincias(paisId));
      } catch {
        toast.error("No se pudieron cargar las provincias");
      } finally {
        setCargandoProvincias(false);
      }
    };

    cargarProvincias();
  }, [paisId]);

  // Cantones dependientes de la provincia

  useEffect(() => {
    setCantones([]);

    if (!paisId || !provinciaId) return;

    const cargarCantones = async () => {
      setCargandoCantones(true);

      try {
        setCantones(await listarCantones(paisId, provinciaId));
      } catch {
        toast.error("No se pudieron cargar los cantones");
      } finally {
        setCargandoCantones(false);
      }
    };

    cargarCantones();
  }, [paisId, provinciaId]);

  return {
    paises,
    provincias,
    cantones,
    cargandoPaises,
    cargandoProvincias,
    cargandoCantones,
  };
}
