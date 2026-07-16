import type { Pessoa } from "../types/Pessoa";

interface ListaPessoasProps {
    pessoas: Pessoa[];
    aoDeletar: (id: number) => void
}

const ListaPessoas = ({ pessoas, aoDeletar }: ListaPessoasProps) => {


    return (
        <ul>
            {pessoas.map((pessoa) => (
                <li key={String(pessoa.id)}> {pessoa.nome} - {pessoa.idade} anos - {pessoa.id}   
                    <button onClick={() => {
                            if (window.confirm(`Tem certeza que deseja excluir ${pessoa.nome}?`)) {
                                aoDeletar(pessoa.id);
                            }}}> Excluir </button>
                </li>
            ))}
        </ul>
    );
}

export { ListaPessoas };