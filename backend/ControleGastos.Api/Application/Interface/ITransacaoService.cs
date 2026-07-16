using ControleGastos.Api.Application.DTOs;

namespace ControleGastos.Api.Application.Interface
{
    public interface ITransacaoService
    {
        Task<TransacaoResponseDto> CriarAsync(TransacaoRequestDto dto);
        Task<IEnumerable<TransacaoResponseDto>> ListarTodasAsync();
        Task<IEnumerable<TransacaoResponseDto>> ListarPorPessoaAsync(int pessoaId);
    }
}