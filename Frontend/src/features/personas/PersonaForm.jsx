import { Hash, User, Users, Mail, Phone } from "lucide-react";
import { useUbicaciones } from "../ubicaciones/useUbicaciones";

const soloDigitos = (texto) => texto.replace(/\D/g, "");
const soloLetras = (texto) => texto.replace(/[^a-zA-ZÀ-ÿñÑ\s]/g, "");

const limpiadores = {
  numeroIdentificacion: soloDigitos,
  nombres: soloLetras,
  apellidos: soloLetras,
  telefono: soloDigitos,
};

const PersonaForm = ({ valores, onCambiar }) => {
  const tipoIdentificacion = valores.tipoIdentificacion ?? "CED";
  const longitudIdentificacion = tipoIdentificacion === "RUC" ? 13 : 10;

  const {
    paises,
    provincias,
    cantones,
    cargandoPaises,
    cargandoProvincias,
    cargandoCantones,
  } = useUbicaciones(valores.paisId, valores.provinciaId);

  const manejarCambio = (e) => {
    const { name, value } = e.target;

    if (name === "paisId") {
      onCambiar("paisId", value);
      onCambiar("provinciaId", "");
      onCambiar("cantonId", "");
      return;
    }

    if (name === "provinciaId") {
      onCambiar("provinciaId", value);
      onCambiar("cantonId", "");
      return;
    }

    const limpiador = limpiadores[name];
    onCambiar(name, limpiador ? limpiador(value) : value);
  };

  return (
    <div className="space-y-4">
      <label className="form-control w-full">
        <span className="label-text mb-1">Tipo de identificación</span>
        <select
          name="tipoIdentificacion"
          className="select select-bordered w-full"
          value={tipoIdentificacion}
          onChange={manejarCambio}
          required
        >
          <option value="CED">Cédula</option>
          <option value="RUC">RUC</option>
        </select>
      </label>

      <label className="input input-bordered flex items-center gap-2 w-full">
        <Hash className="size-4 text-base-content/40" />
        <input
          type="text"
          name="numeroIdentificacion"
          placeholder={
            tipoIdentificacion === "RUC"
              ? "Número de RUC (13 dígitos)"
              : "Número de cédula (10 dígitos)"
          }
          className="grow"
          inputMode="numeric"
          minLength={longitudIdentificacion}
          maxLength={longitudIdentificacion}
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

      <div className="space-y-4 border-t border-base-300 pt-4">
          <h4 className="font-semibold text-primary">Lugar de domicilio</h4>

          <label className="form-control w-full">
            <span className="label-text mb-1">País</span>
            <select
              name="paisId"
              className="select select-bordered w-full"
              value={valores.paisId}
              onChange={manejarCambio}
              disabled={cargandoPaises}
              required
            >
              <option value="">
                {cargandoPaises ? "Cargando países..." : "Seleccione un país"}
              </option>
              {paises.map((pais) => (
                <option key={pais.id} value={pais.id}>
                  {pais.nombre}
                </option>
              ))}
            </select>
          </label>

          <label className="form-control w-full">
            <span className="label-text mb-1">Provincia</span>
            <select
              name="provinciaId"
              className="select select-bordered w-full"
              value={valores.provinciaId}
              onChange={manejarCambio}
              disabled={!valores.paisId || cargandoProvincias}
              required
            >
              <option value="">
                {cargandoProvincias
                  ? "Cargando provincias..."
                  : "Seleccione una provincia"}
              </option>
              {provincias.map((provincia) => (
                <option key={provincia.id} value={provincia.id}>
                  {provincia.nombre}
                </option>
              ))}
            </select>
          </label>

          <label className="form-control w-full">
            <span className="label-text mb-1">Cantón</span>
            <select
              name="cantonId"
              className="select select-bordered w-full"
              value={valores.cantonId}
              onChange={manejarCambio}
              disabled={!valores.provinciaId || cargandoCantones}
              required
            >
              <option value="">
                {cargandoCantones
                  ? "Cargando cantones..."
                  : "Seleccione un cantón"}
              </option>
              {cantones.map((canton) => (
                <option key={canton.id} value={canton.id}>
                  {canton.nombre}
                </option>
              ))}
            </select>
          </label>

          <label className="form-control w-full">
            <span className="label-text mb-1">Dirección</span>
            <input
              type="text"
              name="direccion"
              className="input input-bordered w-full"
              placeholder="Ingrese la dirección del domicilio"
              value={valores.direccion}
              onChange={manejarCambio}
              maxLength={200}
              required
            />
          </label>
      </div>
    </div>
  );
};

export default PersonaForm;
