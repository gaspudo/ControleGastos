using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Application.DTOs
{
    // O que a API recebe para criar uma transação
    public record TransacaoRequestDto(
        [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O ID da pessoa deve ser um identificador válido.")]
        int PessoaId,

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "A descrição deve ter entre {2} e {1} caracteres.")]
        string Descricao,
        
        [Range(typeof(decimal), "0,01", "79228162514264337593543950335", ErrorMessage = "O valor da transação deve ser maior que zero.")]
        [Required(ErrorMessage = "O valor é obrigatório.")]
        decimal Valor,

        [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
        [RegularExpression("^(Receita|Despesa)$", ErrorMessage = "O tipo deve ser exatamente 'Receita' ou 'Despesa'.")]
        string Tipo
    );

    // O que a API retorna para o cliente
    public record TransacaoResponseDto(
        int Id,
        int PessoaId,
        string Descricao,
        decimal Valor,
        string Tipo
    );
}