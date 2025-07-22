// Atributos: Primeiro nome, Último nome
// Regras: Nenhum dos nomes pode ser vazio ou nulo
// Uso: User, Author, Librarian
namespace Biblioteca.Domain.ValueObjects;

public class NomeCompleto : ValueObject
{
    #region Propriedades
    public string PrimeiroNome { get; private set; }
    public string UltimoNome { get; private set; }
    #endregion

    #region Construtores
    //Construtor da classe NomeCompleto que recebe o primeiro e último nome, e verifica se são válidos.
    // Se algum dos nomes for inválido, uma exceção (ArgumentException) é lançada.
    public NomeCompleto(string primeiroNome, string ultimoNome)
    {
        if (string.IsNullOrWhiteSpace(primeiroNome))
            throw new ArgumentException("Primeiro nome não pode ser vazio ou nulo.", nameof(primeiroNome));
        if (string.IsNullOrWhiteSpace(ultimoNome))
            throw new ArgumentException("Último nome não pode ser vazio ou nulo.", nameof(ultimoNome));
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
    }
    #endregion
}