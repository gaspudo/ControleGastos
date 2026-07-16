import { useState } from "react";
import type { PessoaRequest } from "../types/Pessoa";
import { criarPessoa } from "../services/PessoaService";


interface FormularioPessoaProps {
    aoCriar: () => void;
}
const FormularioPessoa = ({aoCriar}: FormularioPessoaProps) => {
    const [nome, setNome] = useState("");
    const [dataNascimento, setDataNascimento] = useState("");

    const handleSubmit = async (evento: React.SubmitEvent) => {
        evento.preventDefault();

        const novaPessoa: PessoaRequest = {nome, dataNascimento};
        await criarPessoa(novaPessoa);

        setNome("");
        setDataNascimento("");
        aoCriar();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input type="text" value={nome} onChange={(e) => setNome(e.target.value)}
            placeholder="Nome" />

            <input type="date" value={dataNascimento} onChange={(e)=> setDataNascimento(e.target.value)} />

            <button type="submit">Criar Pessoa</button>
        </form>
    );
}

export {FormularioPessoa}