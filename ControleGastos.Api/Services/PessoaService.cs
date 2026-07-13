using ControleGastos.Api.Application.DTOs;
using ControleGastos.Api.Application.Interface;
using ControleGastos.Domain.Entities;
using ControleGastos.Domain.Exceptions;
using ControleGastos.Domain.Interfaces;

namespace ControleGastos.Api.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IUnitOfWork _unitOfWork;

        // poderia usar apenas o unitOfWork, porem para ser mais fácil de entender, trouxe as coisas q o service pode fazer separadamente.
        public PessoaService(IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
        {
            _pessoaRepository = pessoaRepository ?? throw new ArgumentNullException(nameof(pessoaRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<PessoaResponseDto> CriarAsync(PessoaRequestDto dto)
        {
            // instanciando o domínio disparando as validações do construtor da Entidade
            var pessoa = new Pessoa(dto.Nome, dto.DataNascimento);

            // usando o repositório isolado para clareza visual
            await _pessoaRepository.CriarAsync(pessoa);

            // calculo da idade para o DTO de retorno ja ocorre dentro da classe
            return new PessoaResponseDto(pessoa.Id, pessoa.Nome, pessoa.Idade);
        }

        public async Task DeletarAsync(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // remove as transações da pessoa primeiro (lado "filho" da relação)
                await _unitOfWork.Transacoes.DeletarPorPessoaAsync(id);

                // então remove a pessoa (lado "pai")
                var sucesso = await _unitOfWork.Pessoas.DeletarAsync(id);
                if (!sucesso)
                    throw new NotFoundException($"Pessoa com ID {id} não foi encontrada.");

                await _unitOfWork.CommitAsync();
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<PessoaResponseDto>> ListarTodosAsync()
        {
            var pessoas = await _pessoaRepository.ListarTodosAsync();

            // Deixa a entidade Pessoa resolver a regra da idade
            return pessoas.Select(pessoa => new PessoaResponseDto(
                pessoa.Id, 
                pessoa.Nome, 
                pessoa.Idade
            ));
        }

        public async Task<PessoaResponseDto> ObterPorIdAsync(int id)
        {
            var pessoa = await _pessoaRepository.ObterPorIdAsync(id) ?? throw new NotFoundException($"Nenhuma pessoa com id {id} foi encontrada");

            return new PessoaResponseDto(id, pessoa.Nome, pessoa.Idade);
        }
    }
}