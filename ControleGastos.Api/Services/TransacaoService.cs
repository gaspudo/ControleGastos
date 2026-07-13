using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Enums;
using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Api.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IPessoaRepository _pessoaRepository;

        public TransacaoService(
            ITransacaoRepository transacaoRepository, 
            IPessoaRepository pessoaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<TransacaoResponseDto> CriarAsync(TransacaoRequestDto dto)
        {
            // garante que a pessoa associada realmente existe
            var pessoa = await _pessoaRepository.ObterPorIdAsync(dto.PessoaId);
            if (pessoa == null)
            {
                throw new NotFoundException($"Não é possível criar a transação: Pessoa com ID {dto.PessoaId} não encontrada.");
            }

            // convertendo a string para o meu enum
            if (!Enum.TryParse<TipoTransacao>(dto.Tipo, ignoreCase: true, out var tipoTransacao))
            {
                throw new RegraDeNegocioException($"Tipo de transação inválido: '{dto.Tipo}'.");
            }

            // instancia o domínio disparando as validações do construtor da Entidade (Ex: Valor maior que zero)
            var transacao = new Transacao(dto.Descricao, dto.Valor, tipoTransacao , dto.PessoaId, pessoa.Idade);

            // persistência simples via repositório isolado
            await _transacaoRepository.CriarAsync(transacao);

            // retornando o DTO mapeado
            return new TransacaoResponseDto(
                transacao.Id, 
                transacao.PessoaId, 
                transacao.Descricao, 
                transacao.Valor, 
                transacao.Tipo.ToString() // Convertendo o Enum interno para a string acordada no DTO
            );
        }

        public async Task<IEnumerable<TransacaoResponseDto>> ListarTodasAsync()
        {
            var transacoes = await _transacaoRepository.ListarTodasAsync();

            return transacoes.Select(t => new TransacaoResponseDto(
                t.Id,
                t.PessoaId,
                t.Descricao,
                t.Valor,
                t.Tipo.ToString()
            ));
        }

        public async Task<IEnumerable<TransacaoResponseDto>> ListarPorPessoaAsync(int pessoaId)
        {
            // Valida se a pessoa existe antes de tentar buscar ou retornar uma lista vazia ambígua
            var pessoa = await _pessoaRepository.ObterPorIdAsync(pessoaId);
            if (pessoa == null)
            {
                throw new NotFoundException($"Pessoa com ID {pessoaId} não encontrada.");
            }

            var transacoes = await _transacaoRepository.ListarPorPessoaAsync(pessoaId);

            return transacoes.Select(t => new TransacaoResponseDto(
                t.Id,
                t.PessoaId,
                t.Descricao,
                t.Valor,
                t.Tipo.ToString()
            ));
        }
    }
}