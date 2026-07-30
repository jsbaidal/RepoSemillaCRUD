import { Route, Routes } from "react-router";
import HomePage from "./pages/HomePage";
import CrearPersonaPage from "./pages/CrearPersonaPage";
import DetallePersonaPage from "./pages/DetallePersonaPage";

function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/detallePersona/:id" element={<DetallePersonaPage />} />
      <Route path="/crearPersona" element={<CrearPersonaPage />} />
    </Routes>
  );
}

export default App;
