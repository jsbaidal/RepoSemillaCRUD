import { Hash, User, Users, Mail, Phone } from "lucide-react";

const soloDigitos = (texto) => texto.replace(/\D/g, "");
const soloLetras = (texto) => texto.replace(/[^a-zA-ZÀ-ÿñÑ\s]/g, "");

const limpiadores = {
  numeroIdentificacion: soloDigitos,
  nombres: soloLetras,
  apellidos: soloLetras,
  telefono: soloDigitos,
};

const PersonaForm = ({ valores, onCambiar }) => {
  const manejarCambio = (e) => {
    const { name, value } = e.target;
    const limpiador = limpiadores[name];
    onCambiar(name, limpiador ? limpiador(value) : value);
  };

  return (
    <div className="space-y-4">
      <label className="input input-bordered flex items-center gap-2 w-full">
        <Hash className="size-4 text-base-content/40" />
        <input
          type="text"
          name="numeroIdentificacion"
          placeholder="Número de identificación (10 dígitos)"
          className="grow"
          inputMode="numeric"
          minLength={10}
          maxLength={10}
          required
          value={valores.numeroIdentificacion}
          onChange={manejarCambio}
        />
      </label>

      <label className="input input-bordered flex items-center gap-2 w-full">
        <User className="size-4 text-base-content/40" />
        <input
          type="text"
          name="nombres"
          placeholder="Nombres"
          className="grow"
          maxLength={20}
          required
          value={valores.nombres}
          onChange={manejarCambio}
        />
      </label>

      <label className="input input-bordered flex items-center gap-2 w-full">
        <Users className="size-4 text-base-content/40" />
        <input
          type="text"
          name="apellidos"
          placeholder="Apellidos"
          className="grow"
          maxLength={20}
          required
          value={valores.apellidos}
          onChange={manejarCambio}
        />
      </label>

      <label className="input input-bordered flex items-center gap-2 w-full">
        <Mail className="size-4 text-base-content/40" />
        <input
          type="email"
          name="email"
          placeholder="correo@ejemplo.com"
          className="grow"
          maxLength={30}
          required
          value={valores.email}
          onChange={manejarCambio}
        />
      </label>

      <label className="input input-bordered flex items-center gap-2 w-full">
        <Phone className="size-4 text-base-content/40" />
        <input
          type="tel"
          name="telefono"
          placeholder="0987654321"
          className="grow"
          inputMode="numeric"
          minLength={10}
          maxLength={10}
          required
          value={valores.telefono}
          onChange={manejarCambio}
        />
      </label>
    </div>
  );
};

export default PersonaForm;
