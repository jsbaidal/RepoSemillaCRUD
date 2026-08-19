import { PlusIcon } from "lucide-react";

const Navbar = ({ alCrear }) => {
  return (
    <header className="bg-base-100 border-b border-primary/20 shadow-sm">
      <div className="mx-auto max-w-6xl p-4">
        <div className="flex items-center justify-between">
          <h1 className="text-2xl font-semibold text-primary tracking-tight">
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
