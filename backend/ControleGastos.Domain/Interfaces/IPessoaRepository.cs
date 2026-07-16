using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa> CriarAsync (Pessoa pessoa);
        Task<IEnumerable<Pessoa>> ListarTodosAsync();
        Task<Pessoa?> ObterPorIdAsync (int id);
        Task<bool> DeletarAsync (int id);
    }
}