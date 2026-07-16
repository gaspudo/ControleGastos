import type { Pessoa } from "../types/Pessoa";
import Lixo from "../assets/lixo.svg";
import "./ListaPessoas.css";

interface ListaPessoasProps {
    pessoas: Pessoa[];
    aoDeletar: (id: number) => void
}

const ListaPessoas = ({ pessoas, aoDeletar }: ListaPessoasProps) => {


    return (
        <ul className="lista-pessoas">
            {pessoas.map((pessoa) => (
                <li className='lista-pessoas-item' key={String(pessoa.id)}> {pessoa.nome} - {pessoa.idade} anos - ID {pessoa.id}   
                    <button className="lista-pessoas-botao-deletar" onClick={() => {
                            if (window.confirm(`Tem certeza que deseja excluir ${pessoa.nome}?`)) {
                                aoDeletar(pessoa.id);
                            }}}> <img src={Lixo} /> </button>
                </li>
            ))}
        </ul>
    );
}

export { ListaPessoas };