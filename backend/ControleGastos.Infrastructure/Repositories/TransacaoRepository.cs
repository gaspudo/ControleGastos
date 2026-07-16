using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly Context _context;

        public TransacaoRepository (Context contexto)
        {
            _context = contexto;
        }
        public async Task<Transacao> CriarAsync(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return transacao;
        }

        public async Task<bool> DeletarPorPessoaAsync(int pessoaId)
        {
            var linhasModificadas = await _context.Transacoes.Where(t => t.PessoaId == pessoaId)
            .ExecuteDeleteAsync();

            return linhasModificadas > 0;
        }

        public async Task<IEnumerable<Transacao>> ListarPorPessoaAsync(int pessoaId)
        {
            var transacoes = await _context.Transacoes.Where(t => t.PessoaId  == pessoaId).ToListAsync();

            return transacoes;
            
        }

        public async Task<IEnumerable<Transacao>> ListarTodasAsync()
        {
            return await _context.Transacoes.ToListAsync();
        }

        public async Task<Transacao?> ObterPorIdAsync(int id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            
            return transacao;
        }
    }
}