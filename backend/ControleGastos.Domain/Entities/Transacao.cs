using System;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    public class Transacao
    {
        public int Id {get; private set;} //private para impedir que seja alterado apos a criacao
        public string Descricao {get; private set;} = string.Empty;
        public decimal Valor {get; private set;}
        public TipoTransacao Tipo {get; private set;}
        public int PessoaId {get;private set;}

        private Transacao() {} // materializacao do entity framework core do banco

        public Transacao (string descricao, decimal valor, TipoTransacao tipo, int pessoaId, int idade)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new RegraDeNegocioException("Descrição não pode ser vazia.");

            if (valor <= 0)
                throw new RegraDeNegocioException("Valor deve ser maior que zero.");

            if (idade < 18 && tipo == TipoTransacao.Receita)
            throw new RegraDeNegocioException("Menores de 18 anos só podem cadastrar despesas.");

            Descricao = descricao;
            Valor = valor;
            Tipo = tipo;
            PessoaId = pessoaId;
        }
    }
}