export interface Transacao {
    id: number;
    pessoaId: number;
    descricao: string;
    valor: number;
    tipo: string; // "Receita" ou "Despesa", igual seu TransacaoResponseDto
}

export interface TransacaoRequest {
    pessoaId: number;
    descricao: string;
    valor: number;
    tipo: string;
}
