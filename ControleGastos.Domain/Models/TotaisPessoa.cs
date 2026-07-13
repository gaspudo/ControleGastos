
namespace ControleGastos.Domain.Models
{
    public record TotaisPessoa
    (
        int PessoaId,
        string Nome,
        decimal TotalReceitas,
        decimal TotalDespesas,
        decimal Saldo
    );
}