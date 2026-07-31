import { Mail, Phone, User } from "lucide-react";

const FormularioPersona = ({
  nombre,
  correo,
  telefono,
  onNombreChange,
  onCorreoChange,
  onTelefonoChange,
}) => {
  return (
    <>
      <label className="input input-bordered flex items-center gap-2 w-full">
        <User className="size-4 text-base-content/40" />
        <input
          type="text"
          placeholder="Nombre de la persona"
          className="grow"
          maxLength={50}
          value={nombre}
          onChange={(e) => onNombreChange(e.target.value.replace(/@/g, ""))}
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
          onChange={(e) => onCorreoChange(e.target.value)}
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
          onChange={(e) => onTelefonoChange(e.target.value.replace(/\D/g, ""))}
        />
      </label>
    </>
  );
};

export default FormularioPersona;
