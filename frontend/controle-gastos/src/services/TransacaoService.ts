import type { Transacao, TransacaoRequest } from "../types/Transacao";

const API_URL_TRANSACOES = `${import.meta.env.VITE_API_URL}/transacoes`;

const buscarTransacoes = async (): Promise<Transacao[]> => {
    const resposta = await fetch(API_URL_TRANSACOES);
    if (!resposta.ok) {
        throw new Error(
            `Erro ao buscar transações. \nErro (${resposta.status}): (${resposta.statusText})`,
        );
    }
    return resposta.json();
};

const criarTransacao = async (
    novaTransacao: TransacaoRequest,
): Promise<Transacao> => {
    const resposta = await fetch(API_URL_TRANSACOES, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novaTransacao),
    });

    if (!resposta.ok) {
        throw new Error(
            `Erro ao criar transação. \nErro (${resposta.status}): (${resposta.statusText})`,
        );
    }

    return resposta.json();
};

export { buscarTransacoes, criarTransacao };
