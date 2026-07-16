using System;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Application.DTOs
{
    public record PessoaRequestDto (
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre {2} e {1} caracteres.")]
        string Nome,

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        DateTime DataNascimento
    );

    // O que a API devolve para o cliente
    public record PessoaResponseDto(
        int Id,
        string Nome,
        int Idade
    );
}