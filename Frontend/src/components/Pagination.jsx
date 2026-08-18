const Pagination = ({ pagina, total, tamanoPagina, alAnterior, alSiguiente }) => {
  const totalPaginas = Math.max(1, Math.ceil(total / tamanoPagina));

  return (
    <div className="flex items-center justify-center gap-4 mt-8">
      <button className="btn btn-outline btn-sm" onClick={alAnterior} disabled={pagina <= 1}>
        ◀ Anterior
      </button>

      <span className="text-base-content/70">
        Página {pagina} de {totalPaginas}
      </span>

      <button
        className="btn btn-outline btn-sm"
        onClick={alSiguiente}
        disabled={pagina >= totalPaginas}
      >
        Siguiente ▶
      </button>
    </div>
  );
};

export default Pagination;
