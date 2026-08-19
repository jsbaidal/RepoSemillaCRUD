import { useState } from "react";
import Navbar from "../components/Navbar";
import Modal from "../components/Modal";
import Pagination from "../components/Pagination";
import PersonaCard from "../features/personas/PersonaCard";
import PersonaForm from "../features/personas/PersonaForm";
import { usePersonas } from "../features/personas/usePersonas";

const VACIO = {
  tipoIdentificacion: "CED",
  numeroIdentificacion: "",
  nombres: "",
  apellidos: "",
  email: "",
  telefono: "",
  paisId: "",
  provinciaId: "",
  cantonId: "",
  direccion: "",
};

const HomePage = () => {
  const {
    personas,
    total,
    pagina,
    tamanoPagina,
    cargando,
    obtener,
    crear,
    editar,
    eliminar,
    irASiguiente,
    irAAnterior,
  } = usePersonas();

  // Estado del formulario
  const [modalAbierto, setModalAbierto] = useState(null);
  const [valores, setValores] = useState(VACIO);
  const [guardando, setGuardando] = useState(false);
  const [errorFormulario, setErrorFormulario] = useState("");

  // Apertura y cierre del modal

  const abrirCrear = () => {
    setErrorFormulario("");
    setValores(VACIO);
    setModalAbierto("crear");
  };

  const abrirEditar = async (persona) => {
    setErrorFormulario("");
    const detalle = await obtener(persona.id);

    setValores({
      ...VACIO,
      tipoIdentificacion: detalle.tipoIdentificacion ?? "CED",
      numeroIdentificacion: detalle.numeroIdentificacion ?? "",
      nombres: detalle.nombres ?? "",
      apellidos: detalle.apellidos ?? "",
      email: detalle.email ?? "",
      telefono: detalle.telefono ?? "",
      paisId: detalle.paisId ? String(detalle.paisId) : "",
      provinciaId: detalle.provinciaId ? String(detalle.provinciaId) : "",
      cantonId: detalle.cantonId ? String(detalle.cantonId) : "",
      direccion: detalle.direccion ?? "",
    });
    setModalAbierto(persona.id);
  };

  const cerrarModal = () => setModalAbierto(null);

  // Actualización del formulario

  const cambiarValor = (campo, valor) => {
    setErrorFormulario("");
    setValores((prev) => ({ ...prev, [campo]: valor }));
  };

  // Envío del formulario

  const enviar = async (e) => {
    e.preventDefault();
    setGuardando(true);
    setErrorFormulario("");
    try {
      const datos = {
        ...valores,
        paisId: Number(valores.paisId),
        provinciaId: Number(valores.provinciaId),
        cantonId: Number(valores.cantonId),
      };

      if (modalAbierto === "crear") {
        await crear(datos);
      } else {
        await editar(modalAbierto, datos);
      }
      cerrarModal();
    } catch (error) {
      setErrorFormulario(error.message);
    } finally {
      setGuardando(false);
    }
  };

  return (
    <div className="min-h-screen bg-base-200/50">
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

        {errorFormulario && (
          <div role="alert" className="alert alert-error mb-4">
            <span>{errorFormulario}</span>
          </div>
        )}

        <form onSubmit={enviar} className="space-y-4">
          <PersonaForm valores={valores} onCambiar={cambiarValor} />

          <button
            type="submit"
            className="btn btn-primary w-full"
            disabled={guardando}
          >
            {guardando ? "Guardando..." : "Guardar"}
          </button>
        </form>
      </Modal>
    </div>
  );
};

export default HomePage;
