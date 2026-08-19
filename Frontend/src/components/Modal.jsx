import { useEffect, useRef } from "react";

const Modal = ({ abierto, alCerrar, children }) => {
  const dialogRef = useRef(null);

  useEffect(() => {
    if (abierto) {
      dialogRef.current?.showModal();
    } else {
      dialogRef.current?.close();
    }
  }, [abierto]);

  return (
    <dialog ref={dialogRef} className="modal" onClose={alCerrar}>
      <div className="modal-box max-h-[90vh] overflow-y-auto border border-base-300 shadow-xl">
        <button
          type="button"
          className="btn btn-sm btn-circle btn-ghost absolute right-2 top-2"
          onClick={alCerrar}
        >
          ✕
        </button>
        {children}
      </div>
      <form method="dialog" className="modal-backdrop">
        <button>cerrar</button>
      </form>
    </dialog>
  );
};

export default Modal;
