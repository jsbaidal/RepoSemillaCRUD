import { useState } from "react";
import Navbar from "../components/Navbar";
import Modal from "../components/Modal";
import Pagination from "../components/Pagination";
import PersonaCard from "../features/personas/PersonaCard";
import PersonaForm from "../features/personas/PersonaForm";
import { usePersonas } from "../features/personas/usePersonas";

const VACIO = {
  numeroIdentificacion: "",
  nombres: "",
  apellidos: "",
  email: "",
  telefono: "",
};

const HomePage = () => {
  const {
    personas,
    total,
    pagina,
    tamanoPagina,
    cargando,
    crear,
    editar,
    eliminar,
    irASiguiente,
    irAAnterior,
  } = usePersonas();

  const [modalAbierto, setModalAbierto] = useState(null);
  const [valores, setValores] = useState(VACIO);
  const [guardando, setGuardando] = useState(false);

  const abrirCrear = () => {
    setValores(VACIO);
    setModalAbierto("crear");
  };

  const abrirEditar = (persona) => {
    setValores({
      numeroIdentificacion: persona.numeroIdentificacion ?? "",
      nombres: persona.nombres ?? "",
      apellidos: persona.apellidos ?? "",
      email: persona.email ?? "",
      telefono: persona.telefono ?? "",
    });
    setModalAbierto(persona.id);
  };

  const cerrarModal = () => setModalAbierto(null);

  const cambiarValor = (campo, valor) => {
    setValores((prev) => ({ ...prev, [campo]: valor }));
  };

  const enviar = async (e) => {
    e.preventDefault();
    setGuardando(true);
    try {
      const datos = { ...valores, tipoIdentificacion: "CED" };
      if (modalAbierto === "crear") {
        await crear(datos);
      } else {
        await editar(modalAbierto, datos);
      }
      cerrarModal();
    } catch {
    } finally {
      setGuardando(false);
    }
  };

  return (
    <div className="min-h-screen">
      <Navbar alCrear={abrirCrear} />

      <div className="max-w-7xl mx-auto p-4 mt-6">
        {cargando && (
          <div className="text-center text-primary py-10">Cargando personas...</div>
        )}

        {!cargando && personas.length === 0 && (
          <div className="text-center text-base-content/60 py-16">
            Sin personas registradas
          </div>
        )}

        {personas.length > 0 && (
          <>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {personas.map((persona) => (
                <PersonaCard
                  key={persona.id}
                  persona={persona}
                  onEditar={abrirEditar}
                  onEliminar={eliminar}
                />
              ))}
            </div>
            <Pagination
              pagina={pagina}
              total={total}
              tamanoPagina={tamanoPagina}
              alAnterior={irAAnterior}
              alSiguiente={irASiguiente}
            />
          </>
        )}
      </div>

      <Modal abierto={modalAbierto !== null} alCerrar={cerrarModal}>
        <h3 className="text-lg font-bold mb-4">
          {modalAbierto === "crear" ? "Nueva persona" : "Editar persona"}
        </h3>
        <form onSubmit={enviar} className="space-y-4">
          <PersonaForm valores={valores} onCambiar={cambiarValor} />
          <button type="submit" className="btn btn-primary w-full" disabled={guardando}>
            {guardando ? "Guardando..." : "Guardar"}
          </button>
        </form>
      </Modal>
    </div>
  );
};

export default HomePage;
