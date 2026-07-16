using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransacoesController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacoesController (ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [HttpPost]
        public async Task<ActionResult<TransacaoResponseDto>> Criar ([FromBody] TransacaoRequestDto request) 
        {
            var transacaoCriada = await _transacaoService.CriarAsync(request);
            return Created($"api/transacoes/{transacaoCriada.Id}", transacaoCriada);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransacaoResponseDto>>> Listar ()
        {
            var transacoes = await _transacaoService.ListarTodasAsync();
            return Ok(transacoes);
        }

        [HttpGet("pessoa/{id}")]
        public async Task <ActionResult<IEnumerable<TransacaoResponseDto>>> BuscarPorPessoa (int id)
        {
            var transacoes = await _transacaoService.ListarPorPessoaAsync(id);
            return Ok(transacoes);
        }


    }
}