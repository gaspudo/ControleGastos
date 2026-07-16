using ControleGastos.Domain.Entities;

namespace ControleGastos.Domain.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<Transacao> CriarAsync (Transacao transacao);
        Task<IEnumerable<Transacao>> ListarTodasAsync ();
        Task<IEnumerable<Transacao>> ListarPorPessoaAsync (int pessoaId);
        Task<Transacao?> ObterPorIdAsync (int id);

        Task<bool> DeletarPorPessoaAsync (int pessoaId);
    }
}