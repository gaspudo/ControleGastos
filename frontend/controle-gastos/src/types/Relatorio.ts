export interface TotaisPessoa {
    pessoaId: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
};

export interface RelatorioTotais {
    pessoas: TotaisPessoa [];
    totalGeralReceitas: number;
    totalGeralDespesas: number;
    saldoGeral: number;
};

