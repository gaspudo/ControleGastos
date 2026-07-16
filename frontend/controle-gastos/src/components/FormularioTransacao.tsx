import { useState } from "react";
import type { TransacaoRequest } from "../types/Transacao";
import type { Pessoa } from "../types/Pessoa";
import { criarTransacao } from "../services/TransacaoService";

interface FormularioTransacaoProps {
    pessoas: Pessoa[];
    aoCriar: () => void;
}

function FormularioTransacao({ pessoas, aoCriar }: FormularioTransacaoProps) {
    const [pessoaId, setPessoaId] = useState<number>(0);
    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState("");
    const [tipo, setTipo] = useState("Despesa");
    const [erro, setErro] = useState<string | null>(null);

    const handleSubmit = async (evento: React.FormEvent) => {
        evento.preventDefault();
        setErro(null);

        if (pessoaId === 0) {
            setErro("Selecione uma pessoa.");
            return;
        }

        try {
            const novaTransacao: TransacaoRequest = { pessoaId, descricao, valor: Number(valor), tipo };
            await criarTransacao(novaTransacao);
            setDescricao("");
            setValor("");
            aoCriar();
        } catch (erroCapturado) {
            setErro((erroCapturado as Error).message);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            {erro && <p style={{color: "red"}}>{erro}</p>}
            <select value={pessoaId} onChange={(e) => setPessoaId(Number(e.target.value))}>
                <option value={0}>Selecione uma pessoa</option>
                {pessoas.map((pessoa) => (
                    <option key={pessoa.id} value={pessoa.id}>
                        {pessoa.nome}
                    </option>
                ))}
            </select>

            <input
                type="text"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
                placeholder="Descrição"
            />

            <input
                type="number"
                step="0.01"
                value={valor}
                onChange={(e) => setValor(e.target.value)}
                placeholder="Valor"
            />

            <select value={tipo} onChange={(e) => setTipo(e.target.value)}>
                <option value="Despesa">Despesa</option>
                <option value="Receita">Receita</option>
            </select>

            <button type="submit">Criar Transação</button>
        </form>
    );
}

export { FormularioTransacao };