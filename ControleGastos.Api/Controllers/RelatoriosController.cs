using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _relatorioService;

        public RelatoriosController (IRelatorioService relatorio)
        {
            _relatorioService = relatorio;
        }
        [HttpGet]
        public async Task<ActionResult<RelatorioTotaisResponseDto>> ObterRelatorio() {
            var relatorio = await _relatorioService.ObterTotaisAsync();
            return Ok(relatorio);
        }
    }
}