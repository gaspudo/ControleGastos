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
import { TabelaTotais } from './components/TabelaTotais';
import { buscarRelatorio } from './services/RelatorioService';
import type { RelatorioTotais } from './types/Relatorio';

function App() {
  const [pessoas, setPessoas] = useState<Pessoa[]>([]);
  const [transacoes, setTransacoes] = useState<Transacao[]>([]);
  const [relatorio, setRelatorio] = useState<RelatorioTotais | null>(null);

  const carregarPessoas = async () => {
    const dados = await buscarPessoas();
    await carregarRelatorio();
    setPessoas(dados);
    };

  useEffect(()=> {
    carregarPessoas();
    carregarRelatorio();
    carregarTransacoes();
  }, []);

  const handleDeletar = async (id: number) => {
    await deletarPessoa(id);
    carregarPessoas();
    carregarTransacoes();
    carregarRelatorio();
  };

  const carregarTransacoes = async () => {
    const dados = await buscarTransacoes();
    setTransacoes(dados);
  };
  const handleCarregarTransacoes = async () => {
    await carregarPessoas();
    await carregarTransacoes();
    await carregarRelatorio();
  }

  const carregarRelatorio = async () => {
    const dados = await buscarRelatorio();
    setRelatorio(dados);
  }

  return (
    <>
      <h1 className='titulo'>Controle Gastos</h1>
      <section>
        <h2>Pessoas</h2>
        <FormularioPessoa aoCriar={carregarPessoas}/>
        <ListaPessoas pessoas={pessoas} aoDeletar={handleDeletar}/>
      </section>

      <section>
        <h2>Transações</h2>
        <FormularioTransacao pessoas={pessoas} aoCriar={handleCarregarTransacoes} />
        <ListaTransacoes transacoes={transacoes} />
      </section>
      <section>
        <h2>Totais</h2>
        <TabelaTotais relatorio={relatorio}  />
      </section>
    </>
  )
}

export default App
