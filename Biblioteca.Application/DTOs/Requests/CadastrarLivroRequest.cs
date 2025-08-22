using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Application.DTOs.Requests
{
    public class CadastrarLivroRequest
    {
        public string Titulo { get; set; }
        public AutorRequest Autor { get; set; }
        public string ISBN { get; set; }
        public int AnoPublicacao { get; set; }
        public int NumeroPaginas { get; set; }
        public string? Descricao { get; set; }
        public List<CategoriaRequest> Categorias { get; set; }
    }

    public class AutorRequest
    {
        public Guid Id { get; set; }
        public NomeCompletoRequest NomeCompleto { get; set; }
        public EmailRequest Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
    public class NomeCompletoRequest
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
    }
    public class EmailRequest
    {
        public string EnderecoEmail { get; set; }
    }

    public class CategoriaRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Tipo { get; set; }
    }
}