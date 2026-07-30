import Navbar from "../components/Navbar";
import TarjetaPersona from "../components/TarjetaPersona";
import PersonaNoEncontrada from "../components/PersonaNoEncontrada";
import api from "../lib/axios";
import { useState, useEffect } from "react";
import toast from "react-hot-toast";

const HomePage = () => {
  const [cargando, setCargando] = useState(false);
  const [listaPersonas, setListaPersonas] = useState([]);

  useEffect(() => {
    const traerPersonas = async () => {
      setCargando(true);
      try {
        const respuesta = await api.get("/");
        setListaPersonas(respuesta.data.listaData ?? []);
      } catch (error) {
        console.error("Error trayendo personas", error);
        toast.error("No se pudieron cargar las personas");
      } finally {
        setCargando(false);
      }
    };

    traerPersonas();
  }, []);
  return (
    <div className="min-h-screen">
      <Navbar />

      <div className="max-w-7xl mx-auto p-4 mt-6">
        {cargando && (
          <div className="text-center text-primary py-10 ">
            Cargado personas..
          </div>
        )}
        {!cargando && listaPersonas.length === 0 && <PersonaNoEncontrada />}
        {listaPersonas.length > 0 && (
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 ">
            {listaPersonas.map((persona) => (
              <TarjetaPersona
                key={persona.id}
                persona={persona}
                onDelete={(id) =>
                  setListaPersonas((prev) => prev.filter((p) => p.id !== id))
                }
              />
            ))}
          </div>
        )}
      </div>
    </div>
  );
};

export default HomePage;
