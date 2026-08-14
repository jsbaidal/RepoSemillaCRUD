import { PlusIcon } from "lucide-react";

const Navbar = ({ alCrear }) => {
  return (
    <header className="bg-base-300 border-b border-base-content/10">
      <div className="mx-auto max-w-6xl p-4">
        <div className="flex items-center justify-between">
          <h1 className="text-3xl font-bold text-primary font-mono tracking-tight">
            App web de personas
          </h1>
          <button className="btn btn-primary" onClick={alCrear}>
            <PlusIcon className="size-5" />
            <span>Nueva Persona</span>
          </button>
        </div>
      </div>
    </header>
  );
};

export default Navbar;
