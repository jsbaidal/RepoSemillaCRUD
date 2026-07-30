import { Link } from "react-router";
import { PenSquareIcon, Trash2Icon } from "lucide-react";
import toast from "react-hot-toast";
import api from "../lib/axios";

const TarjetaPersona = ({ persona, onDelete }) => {
  const handleDelete = async (e) => {
    e.preventDefault();

    if (!window.confirm("¿Seguro que quieres eliminar esta persona?")) return;

    try {
      await api.delete(`/personas/${persona.id}`);
      toast.success("Persona eliminada");
      onDelete(persona.id);
    } catch (error) {
      console.log("Error eliminando persona", error);
      toast.error("No se pudo eliminar la persona");
    }
  };

  return (
    <Link
      to={`/detallePersona/${persona.id}`}
      className="card bg-base-100 hover:shadow-lg transition-all duration-200
        border-t-4 border-solid border-[#00FF9D]"
    >
      <div className="card-body">
        <h3 className="card-title text-base-content">{persona.nombre}</h3>
        <p className="text-base-content/70 line-clamp-3">{persona.correo}</p>
        <div className="card-actions justify-between items-center mt-4">
          <div className="flex items-center gap-1">
            <PenSquareIcon className="size-4" />
            <button className="btn btn-ghost btn-xs text-error" onClick={handleDelete}>
              <Trash2Icon className="size-4" />
            </button>
          </div>
        </div>
      </div>
    </Link>
  );
};

export default TarjetaPersona;
