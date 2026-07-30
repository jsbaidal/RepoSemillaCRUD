import { ArrowLeftIcon, Mail, Phone, User, UserPen } from "lucide-react";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import { Link, useNavigate, useParams } from "react-router";
import api from "../lib/axios";

const DetallePersonaPage = () => {
  const [nombre, setNombre] = useState("");
  const [correo, setCorreo] = useState("");
  const [telefono, setTelefono] = useState("");
  const [cargando, setCargando] = useState(true);
  const [guardando, setGuardando] = useState(false);

  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    const traerPersona = async () => {
      try {
        const respuesta = await api.get(`/personas/${id}`);
        setNombre(respuesta.data.data.nombre);
        setCorreo(respuesta.data.data.correo);
        setTelefono(respuesta.data.data.telefono);
      } catch (error) {
        console.log("Error trayendo persona", error);
        toast.error("No se pudo cargar la persona");
      } finally {
        setCargando(false);
      }
    };

    traerPersona();
  }, [id]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!nombre.trim() || !correo.trim() || !telefono.trim()) {
      toast.error("Todos los campos son obligatorios");
      return;
    }

    setGuardando(true);
    try {
      await api.put(`/personas/${id}`, { nombre, correo, telefono });
      toast.success("Persona actualizada correctamente!");
      navigate("/");
    } catch (error) {
      console.log("Error actualizando persona", error);
      toast.error("No se pudo actualizar la persona");
    } finally {
      setGuardando(false);
    }
  };

  if (cargando) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <span className="loading loading-spinner loading-lg" />
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-base-200">
      <div className="container mx-auto px-4 py-8">
        <div className="max-w-md mx-auto">
          <Link to={"/"} className="btn btn-ghost btn-sm mb-6">
            <ArrowLeftIcon className="size-4" />
            Volver
          </Link>

          <div className="card bg-base-100 shadow-xl border border-base-content/10">
            <div className="card-body items-center text-center">
              <div className="size-14 rounded-full bg-primary/10 flex items-center justify-center mb-2">
                <UserPen className="size-7 text-primary" />
              </div>
              <h2 className="card-title text-2xl">Editar Persona</h2>
              <p className="text-base-content/60 text-sm mb-4">
                Actualiza los datos de la persona
              </p>

              <form onSubmit={handleSubmit} className="w-full space-y-4">
                <label className="input input-bordered flex items-center gap-2 w-full">
                  <User className="size-4 text-base-content/40" />
                  <input
                    type="text"
                    placeholder="Nombre de la persona"
                    className="grow"
                    maxLength={50}
                    value={nombre}
                    onChange={(e) => setNombre(e.target.value.replace(/@/g, ""))}
                  />
                </label>

                <label className="input input-bordered flex items-center gap-2 w-full">
                  <Mail className="size-4 text-base-content/40" />
                  <input
                    type="email"
                    placeholder="correo@ejemplo.com"
                    className="grow"
                    maxLength={100}
                    value={correo}
                    onChange={(e) => setCorreo(e.target.value)}
                  />
                </label>

                <label className="input input-bordered flex items-center gap-2 w-full">
                  <Phone className="size-4 text-base-content/40" />
                  <input
                    type="tel"
                    placeholder="0987654321"
                    className="grow"
                    inputMode="numeric"
                    maxLength={10}
                    value={telefono}
                    onChange={(e) =>
                      setTelefono(e.target.value.replace(/\D/g, ""))
                    }
                  />
                </label>

                <button
                  type="submit"
                  className="btn btn-primary w-full mt-2"
                  disabled={guardando}
                >
                  {guardando ? "Guardando..." : "Guardar Cambios"}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DetallePersonaPage;
