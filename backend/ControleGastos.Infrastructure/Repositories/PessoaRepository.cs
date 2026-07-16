using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Infrastructure.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly Context _context;
        public PessoaRepository (Context context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Pessoa>> ListarTodosAsync()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            return pessoas;
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id)
        {
            var pessoaBuscada = await _context.Pessoas.FindAsync(id);
            
            return pessoaBuscada;
        }

        public async Task<Pessoa> CriarAsync(Pessoa pessoa)
        {
            
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return pessoa;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var pessoaDeletada = await _context.Pessoas.FindAsync(id);

            if (pessoaDeletada == null)
                return false;

            _context.Pessoas.Remove(pessoaDeletada);
            await _context.SaveChangesAsync();

            return true;

            
        }
    }
}