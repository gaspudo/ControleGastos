import { useState } from "react";
import type { PessoaRequest } from "../types/Pessoa";
import { criarPessoa } from "../services/PessoaService";
import './FormularioPessoa.css';

interface FormularioPessoaProps {
    aoCriar: () => void;
}
const FormularioPessoa = ({ aoCriar }: FormularioPessoaProps) => {
    const [nome, setNome] = useState("");
    const [dataNascimento, setDataNascimento] = useState("");
    const [erro, setErro] = useState<string | null>(null);

    const handleSubmit = async (evento: React.FormEvent) => {
        evento.preventDefault();
        setErro(null);

        try {
            const novaPessoa: PessoaRequest = { nome, dataNascimento };
            await criarPessoa(novaPessoa);
            setNome("");
            setDataNascimento("");
            aoCriar();
        } catch (erroCapturado) {
            setErro((erroCapturado as Error).message);
        }
    };

    return (
        <div className="formulario-pessoa-wrapper">
            {erro && <p className="formulario-pessoa-erro">{erro}</p>}
            <form className="formulario-pessoa" onSubmit={handleSubmit}>
                <input type="text" value={nome} onChange={(e) => setNome(e.target.value)}
                    placeholder="Nome" />

                <input type="date" value={dataNascimento} onChange={(e) => setDataNascimento(e.target.value)} />

                <button type="submit">Criar Pessoa</button>
            </form>
        </div>
    );
}

export { FormularioPessoa }