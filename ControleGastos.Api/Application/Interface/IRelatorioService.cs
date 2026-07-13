using ControleGastos.Api.Application.DTOs;

namespace ControleGastos.Api.Application.Interface
{
    public interface IRelatorioService
    {
        Task<RelatorioTotaisResponseDto> ObterTotaisAsync();
    }
}