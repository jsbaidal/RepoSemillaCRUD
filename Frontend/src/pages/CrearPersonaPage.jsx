import { ArrowLeftIcon, Mail, Phone, User, UserPlus } from "lucide-react";
import { useState } from "react";
import toast from "react-hot-toast";
import { Link, useNavigate } from "react-router";
import api from "../lib/axios";

const CrearPersonaPage = () => {
  const [nombre, setNombre] = useState("");
  const [correo, setCorreo] = useState("");
  const [telefono, setTelefono] = useState("");
  const [loading, setLoading] = useState(false);

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!nombre.trim() || !correo.trim() || !telefono.trim()) {
      toast.error("Todos los campos son obligatorios");
      return;
    }

    setLoading(true);
    try {
      await api.post("/personas", { nombre, correo, telefono });

      toast.success("Persona creada correctamente!");
      navigate("/");
    } catch (error) {
      console.log("Error creando persona", error);
      toast.error("No se pudo crear la persona");
    } finally {
      setLoading(false);
    }
  };

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
                <UserPlus className="size-7 text-primary" />
              </div>
              <h2 className="card-title text-2xl">Nueva Persona</h2>
              <p className="text-base-content/60 text-sm mb-4">
                Completa los datos para registrarla
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
                  disabled={loading}
                >
                  {loading ? "Creando..." : "Crear Persona"}
                </button>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default CrearPersonaPage;
