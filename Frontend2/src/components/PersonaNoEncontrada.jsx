import { UsersIcon } from "lucide-react";
import { Link } from "react-router";

const PersonaNoEncontrada = () => {
  return (
    <div className="flex flex-col items-center justify-center py-16 space-y-6 max-w-md mx-auto text-center">
      <div className="bg-primary/10 rounded-full p-8">
        <UsersIcon className="size-10 text-primary" />
      </div>
      <h3 className="text-2xl font-bold">Sin personas registradas</h3>
      <p className="text-base-content/70">
        Crea una nueva persona para empezar a registrarla.
      </p>
      <Link to="/crearPersona" className="btn btn-primary">
        Crear Persona
      </Link>
    </div>
  );
};
export default PersonaNoEncontrada;
