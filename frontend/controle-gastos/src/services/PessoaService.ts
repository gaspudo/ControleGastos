import type { Pessoa, PessoaRequest } from "../types/Pessoa";

const API_URL_PESSOAS=`${import.meta.env.VITE_API_URL}/pessoas`;

const buscarPessoas = async (): Promise<Pessoa[]> => {
    const resposta = await fetch(API_URL_PESSOAS);
    if (!resposta.ok) {
        throw new Error(`Erro ao buscar pessoas. \nErro (${resposta.status}): (${resposta.statusText})`);
    }
    return resposta.json();
};

const criarPessoa = async(novaPessoa: PessoaRequest): Promise<Pessoa> => {
    const resposta = await fetch(API_URL_PESSOAS, {
        method: "POST",
        headers: {
            "Content-type":"application/json",
        },
        body: JSON.stringify(novaPessoa),
    });

    if(!resposta.ok)
        throw new Error(`Erro ao criar pessoa.\nErro (${resposta.status}): (${resposta.body})`);

    return resposta.json();
};

const deletarPessoa = async (id: number): Promise<void> => {
    const resposta = await fetch(`${API_URL_PESSOAS}/${id}`, {
        method: "DELETE",
    });

    if (!resposta.ok)
        throw new Error(`Erro ao deletar pessoa. \nErro (${resposta.status}): (${resposta.body})`);
};

export {
    buscarPessoas,
    criarPessoa,
    deletarPessoa
}