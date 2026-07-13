using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Api.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepository _relatorioRepository;
        public RelatorioService(IRelatorioRepository relatorioRepository)
        {
            _relatorioRepository = relatorioRepository;
        }

        public async Task<RelatorioTotaisResponseDto> ObterTotaisAsync()
        {
            // busca dos dados agrupados q virao do repositorio
            var totaisDoRepositorio = await _relatorioRepository.ObterTotaisPorPessoaAsync();

            var listaPessoasDto = totaisDoRepositorio.Select(t => new TotaisPessoaResponseDto(
                t.PessoaId,
                t.Nome,
                t.TotalReceitas,
                t.TotalDespesas,
                t.Saldo
            )).ToList(); // .ToList() aq para evitar múltiplas iterações nas somas abaixo

            // agregando e consolidando os totais gerais em memória
            var totalGeralReceitas = listaPessoasDto.Sum(p => p.TotalReceitas);
            var totalGeralDespesas = listaPessoasDto.Sum(p => p.TotalDespesas);
            var saldoGeral = totalGeralReceitas - totalGeralDespesas;

            // retorna o relatório consolidado completo
            return new RelatorioTotaisResponseDto(
                listaPessoasDto,
                totalGeralReceitas,
                totalGeralDespesas,
                saldoGeral
            );
        }
    }
}