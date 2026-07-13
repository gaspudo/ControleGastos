using ControleGastos.Domain.Models;

namespace ControleGastos.Domain.Interfaces
{
    public interface IRelatorioRepository
    {
        Task<IEnumerable<TotaisPessoa>> ObterTotaisPorPessoaAsync ();
    }
}