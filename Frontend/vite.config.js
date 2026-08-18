import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";
import tailwindcss from "@tailwindcss/vite";

// El puerto 5175 coincide con el origen permitido por el CORS de Backend2.
export default defineConfig({
  plugins: [react(), tailwindcss()],
  server: { port: 5175 },
});
