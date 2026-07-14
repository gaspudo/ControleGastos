using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;
using Xunit;

namespace ControleGastos.Tests
{
    public class TesteTransacao
    {
        [Fact]
    public void LancarExcecao_MenorDeIdade_Receita()
    {
        // arrange
        string descricao = "Salario";
        decimal valor = 1000m;
        TipoTransacao tipo = TipoTransacao.Receita;
        int idade = 17, pessoaId = 1;

        // Act + Assert
        Assert.Throws<RegraDeNegocioException> (() => new Transacao(descricao, valor, tipo, pessoaId, idade));
        
    }

    [Fact]
    public void CriarNormalmente_MenorDeIdade_Despesa()
    {
        // arrange
        string descricao = "Compra do mes";
        decimal valor = 300.93m;
        TipoTransacao tipo = TipoTransacao.Despesa;
        int idade = 17, pessoaId = 1;

        // Act
        var transacao = new Transacao (descricao, valor, tipo, pessoaId, idade);
        var resultadoEsperado = "Compra do mes";
        // usando a propriedade descricao para provar que a transacao foi criada normalmente

        // assert
        Assert.Equal(resultadoEsperado, transacao.Descricao); 
    }
    }
}