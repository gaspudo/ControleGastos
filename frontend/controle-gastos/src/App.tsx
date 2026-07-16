import { useState, useEffect } from 'react'
import './App.css'
import type { Pessoa } from './types/Pessoa';
import { buscarPessoas, deletarPessoa } from './services/PessoaService';
import { ListaPessoas } from './components/ListaPessoas';
import { FormularioPessoa } from './components/FormularioPessoa';
import { buscarTransacoes } from './services/TransacaoService';
import type { Transacao } from './types/Transacao';
import { ListaTransacoes } from './components/ListaTransacoes';
import { FormularioTransacao } from './components/FormularioTransacao';

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const carregarPessoas = async () => {
    const dados = await buscarPessoas();
    setPessoas(dados);
  };

  useEffect(()=> {
    carregarPessoas();
  }, []);

  const handleDeletar = async (id: number) => {
    await deletarPessoa(id);
    carregarPessoas();
    carregarTransacoes();
  };

  const carregarTransacoes = async () => {
    const dados = await buscarTransacoes();
    setTransacoes(dados);
  };

  return (
    <>
      <h1>Controle Gastos</h1>
      <FormularioPessoa aoCriar={carregarPessoas}/>
      <ListaPessoas pessoas={pessoas} aoDeletar={handleDeletar}/>
      <FormularioTransacao pessoas={pessoas} aoCriar={carregarTransacoes} />
      <ListaTransacoes transacoes={transacoes} />
    </>
  )
}

export default App
