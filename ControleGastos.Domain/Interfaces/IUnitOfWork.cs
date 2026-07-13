
namespace ControleGastos.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPessoaRepository Pessoas {get;}
        ITransacaoRepository Transacoes {get;}

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}