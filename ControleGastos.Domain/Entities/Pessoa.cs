using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ControleGastos.Domain.Exceptions;

namespace ControleGastos.Domain.Entities
{
    public class Pessoa
    {
        public string Nome { get; private set; }
        public int Id { get; private set; }
        public DateTime DataNascimento { get; private set; }
        [NotMapped] // para o banco nao criar tabela de idade, pq nao sabe calcular segundo logica do codigo
        public int Idade => CalcularIdade();

        private Pessoa() { } // materializacao p/ EF Core

        public Pessoa(string nome, DateTime dataNascimento)
        {

            if(string.IsNullOrWhiteSpace(nome))
                throw new RegraDeNegocioException("Nome não pode ser vazio!!");
            if (dataNascimento > DateTime.Today)
                throw new RegraDeNegocioException("Data de nascimento não pode ser no futuro.");

            if (dataNascimento < DateTime.Today.AddYears(-120))
                throw new RegraDeNegocioException("Data de nascimento inválida.");

            Nome = nome;
            DataNascimento = dataNascimento;
        }


        // metodo para atualizar idade conforme o tempo passa, trazendo maior robustez
        private int CalcularIdade()
        {
            if (DataNascimento == default)
                return 0;

            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.Date > hoje.AddYears(-idade))
                idade--;

            return idade;
        }
    }
}