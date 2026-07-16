using ControleGastos.Api.Application.DTOs;

namespace ControleGastos.Api.Application.Interface
{
    public interface IPessoaService
    {
        Task<PessoaResponseDto> CriarAsync(PessoaRequestDto dto);
        Task<IEnumerable<PessoaResponseDto>> ListarTodosAsync();
        Task DeletarAsync(int id); 
        Task<PessoaResponseDto> ObterPorIdAsync(int id);
    }
}