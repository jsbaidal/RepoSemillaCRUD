import { PenSquareIcon, Trash2Icon } from "lucide-react";

const PersonaCard = ({ persona, onEditar, onEliminar }) => {
  const handleEliminar = () => {
    if (!window.confirm("¿Seguro que quieres eliminar esta persona?")) return;
    onEliminar(persona.id);
  };

  return (
    <div className="card bg-base-100 hover:shadow-lg transition-all duration-200 border-t-4 border-solid border-[#00FF9D]">
      <div className="card-body">
        <div className="flex items-start justify-between gap-2">
          <h3 className="card-title text-base-content">
            {persona.nombres} {persona.apellidos}
          </h3>
          <span className="badge badge-outline whitespace-nowrap">
            {persona.tipoIdentificacion} · {persona.numeroIdentificacion}
          </span>
        </div>

        <p className="text-base-content/70">{persona.email}</p>
        <p className="text-base-content/70">{persona.telefono}</p>

        <div className="card-actions justify-end items-center mt-4 gap-1">
          <button className="btn btn-ghost btn-xs" onClick={() => onEditar(persona)}>
            <PenSquareIcon className="size-4" />
          </button>
          <button className="btn btn-ghost btn-xs text-error" onClick={handleEliminar}>
            <Trash2Icon className="size-4" />
          </button>
        </div>
      </div>
    </div>
  );
};

export default PersonaCard;
