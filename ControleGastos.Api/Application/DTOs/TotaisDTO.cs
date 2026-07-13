using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControleGastos.Api.Application.DTOs
{
    public record TotaisPessoaResponseDto( 
        int PessoaId, 
        string Nome, 
        decimal TotalReceitas, 
        decimal TotalDespesas, 
        decimal Saldo);

    public record RelatorioTotaisResponseDto(
        IEnumerable<TotaisPessoaResponseDto> Pessoas,
        decimal TotalGeralReceitas,
        decimal TotalGeralDespesas,
        decimal SaldoGeral
);
}