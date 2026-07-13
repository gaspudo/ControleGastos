using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoasController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpPost]
        public async Task<ActionResult<PessoaResponseDto>> Criar([FromBody] PessoaRequestDto dto)
        {
            var pessoaCriada = await _pessoaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = pessoaCriada.Id }, pessoaCriada);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaResponseDto>>> Listar()
        {
            var pessoas = await _pessoaService.ListarTodosAsync();
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponseDto>> ObterPorId(int id)
        {
            var pessoa = await _pessoaService.ObterPorIdAsync(id);
            return Ok(pessoa);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _pessoaService.DeletarAsync(id);
            return NoContent();
        }
    }
}