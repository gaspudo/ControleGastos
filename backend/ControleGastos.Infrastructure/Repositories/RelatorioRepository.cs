using ControleGastos.Domain.Interfaces;
using ControleGastos.Domain.Models;
using ControleGastos.Infrastructure.Data;
using ControleGastos.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using ControleGastos.Infrastructure.Repositories;
using ControleGastos.Domain.Entities;

namespace ControleGastos.Infrastructure.Repositories
{
    public class RelatorioRepository : IRelatorioRepository
    {
        private readonly Context _context;
        private readonly IPessoaRepository _pessoaRepository;

        public RelatorioRepository (Context contexto, IPessoaRepository repository)
        {
            _context = contexto;
            _pessoaRepository = repository;
        }
        public async Task<IEnumerable<TotaisPessoa>> ObterTotaisPorPessoaAsync()
        {
            
            // busca os totais de apenas quem tem transação
            var totaisPorTransacoes = await _context.Transacoes
                .GroupBy(t => t.PessoaId)
                .Select(g => new
                {
                    PessoaId = g.Key,
                    TotalReceitas = g.Where(t => t.Tipo == TipoTransacao.Receita).Sum(t => (decimal?)t.Valor) ?? 0,
                    TotalDespesas = g.Where(t => t.Tipo == TipoTransacao.Despesa).Sum(t => (decimal?)t.Valor) ?? 0
                })
                .ToListAsync();

            // lista completa de pessoas (aqui entram as que n tem transação)
            var todasAsPessoas = await _pessoaRepository.ListarTodosAsync();

            // junta as duas listas com GroupJoin + DefaultIfEmpty
            var resultadoFinal = todasAsPessoas
                .GroupJoin(
                    totaisPorTransacoes,
                    pessoa => pessoa.Id, 
                    total => total.PessoaId,
                    (pessoa, totaisCorrespondentes) => new 
                    {
                        Pessoa = pessoa,
                        // Usamos o DefaultIfEmpty para garantir que se for null retorne um objeto zerado.
                        Valores = totaisCorrespondentes.DefaultIfEmpty(new { PessoaId = pessoa.Id, TotalReceitas = 0m, TotalDespesas = 0m }).First()
                    }
                )
                .Select(p => new TotaisPessoa(
                p.Pessoa.Id,
                p.Pessoa.Nome,
                p.Valores.TotalReceitas,
                p.Valores.TotalDespesas,
                p.Valores.TotalReceitas - p.Valores.TotalDespesas
            ))
            .ToList();

            return resultadoFinal;
        }
    }
}