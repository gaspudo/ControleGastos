import type { Transacao } from "../types/Transacao";

interface ListaTransacoesProps {
    transacoes: Transacao[];
}

function ListaTransacoes({ transacoes }: ListaTransacoesProps) {
    return (
        <ul>
            {transacoes.map((t) => (
                <li key={t.id}>
                    {t.descricao} — {t.tipo} — R$ {t.valor.toFixed(2)} (Pessoa {t.pessoaId})
                </li>
            ))}
        </ul>
    );
}

export { ListaTransacoes };