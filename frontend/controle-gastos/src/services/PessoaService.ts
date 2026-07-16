import type { Pessoa, PessoaRequest } from "../types/Pessoa";

const API_URL_PESSOAS = `${import.meta.env.VITE_API_URL}/pessoas`;

const buscarPessoas = async (): Promise<Pessoa[]> => {
    const resposta = await fetch(API_URL_PESSOAS);
    if (!resposta.ok) {
        const corpoErro = await resposta.json();
        throw new Error(corpoErro.message ?? "Erro ao buscar pessoas.");
    }
    return resposta.json();
};

const criarPessoa = async (novaPessoa: PessoaRequest): Promise<Pessoa> => {
    const resposta = await fetch(API_URL_PESSOAS, {
        method: "POST",
        headers: {
            "Content-type": "application/json",
        },
        body: JSON.stringify(novaPessoa),
    });

    if (!resposta.ok) {
        const corpoErro = await resposta.json();
        throw new Error(corpoErro.message ?? "Erro ao criar pessoa.");
    }

    return resposta.json();
};

const deletarPessoa = async (id: number): Promise<void> => {
    const resposta = await fetch(`${API_URL_PESSOAS}/${id}`, {
        method: "DELETE",
    });

    if (!resposta.ok) {
        const corpoErro = await resposta.json();
        throw new Error(corpoErro.message ?? "Erro ao deletar pessoa.");
    };
};

export {
    buscarPessoas,
    criarPessoa,
    deletarPessoa
}