using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;
using Xunit;

namespace ControleGastos.Tests;

public class TestePessoa
{

    [Fact]
    public void CalcularIdade_QuandoJaFezAniversarioEsteAno_RetornaIdadeCorreta()
    {
        // Arrange
        // pessoa nasceu ha 20 anos e ja fez aniversario esse ano
        var dataNascimento = DateTime.Today.AddYears(-20).AddMonths(-1);

        // Act
        var pessoa = new Pessoa("Teste", dataNascimento);

        // Assert
        Assert.Equal(20, pessoa.Idade);
    }

    [Fact]
    public void CalcularIdade_QuandoAindaNaoFezAniversarioEsteAno_RetornaIdadeCorreta()
    {
        // Arrange
        // pessoa nasceu 20 anos atrás, mas ainda n fez aniversário
        var dataNascimento = DateTime.Today.AddYears(-20).AddMonths(1);

        // Act
        var pessoa = new Pessoa("Teste", dataNascimento);

        // Assert
        Assert.Equal(19, pessoa.Idade); // ainda não completou os 20
    }

    [Fact]
    public void CalcularIdade_QuandoFazAniversarioHoje_RetornaIdadeCorreta()
    {
        // Arrange
        // pessoa faz aniversario HOJE
        var dataNascimento = DateTime.Today.AddYears(-20);

        // act
        var pessoa = new Pessoa("Teste", dataNascimento);

        // assert
        Assert.Equal(20, pessoa.Idade);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void LancarExcecao_QuandoNomeInvalido(string nome)
    {
        var dataNascimento = DateTime.Today.AddYears(-30);
        Assert.Throws<RegraDeNegocioException>(() => new Pessoa(nome, dataNascimento));
    }

    [Fact]
    public void LancarExcecao_QuandoData_NoFuturo()
    {
        // arrange
        string nome = "pessoa";
        var data = DateTime.Today.AddYears(1);
        
        // act + assert
        Assert.Throws<RegraDeNegocioException> (() => new Pessoa (nome, data));
    }

    [Fact]
    public void LancarExcecao_QuandoData_MuitoAntiga()
    {
        //arrange
        string nome = "nome";
        var data = DateTime.Today.AddYears(-400);

        // act + assert
        Assert.Throws<RegraDeNegocioException> (() => new Pessoa(nome, data));
    }

}
