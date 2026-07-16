import type { RelatorioTotais } from "../types/Relatorio";
import "./TabelaTotais.css"

interface TabelaTotaisProps {
    relatorio: RelatorioTotais | null;
}

function TabelaTotais({ relatorio }: TabelaTotaisProps) {
    if (!relatorio) {
        return <p>Carregando totais...</p>;
    }

    return (
        <table className="tabela-totais">
            <thead>
                <tr>
                    <th>Pessoa</th>
                    <th>Receitas</th>
                    <th>Despesas</th>
                    <th>Saldo</th>
                </tr>
            </thead>
            <tbody>
                {relatorio.pessoas.map((pessoa) => (
                    <tr key={pessoa.pessoaId}>
                        <td>{pessoa.nome}</td>
                        <td>R$ {pessoa.totalReceitas.toFixed(2)}</td>
                        <td>R$ {pessoa.totalDespesas.toFixed(2)}</td>
                        <td>R$ {pessoa.saldo.toFixed(2)}</td>
                    </tr>
                ))}
            </tbody>
            <tfoot>
                <tr>
                    <td><strong>Total Geral</strong></td>
                    <td>R$ {relatorio.totalGeralReceitas.toFixed(2)}</td>
                    <td>R$ {relatorio.totalGeralDespesas.toFixed(2)}</td>
                    <td>R$ {relatorio.saldoGeral.toFixed(2)}</td>
                </tr>
            </tfoot>
        </table>
    );
}

export { TabelaTotais };