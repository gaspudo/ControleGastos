import type { RelatorioTotais } from "../types/Relatorio";

const API_URL_RELATORIO = `${import.meta.env.VITE_API_URL}/relatorios`;

const buscarRelatorio = async (): Promise<RelatorioTotais> => {
    const resposta = await fetch(API_URL_RELATORIO);
    if (!resposta.ok) {
        const corpoErro = await resposta.json();
        throw new Error(corpoErro.message ?? "Erro ao buscar relatório.");
    }
    return resposta.json();
}

export { buscarRelatorio };