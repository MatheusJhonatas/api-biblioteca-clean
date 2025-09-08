// Atributos: Endereço de e-mail
// Regras: Deve conter formato válido (ex: nome@dominio.com)
// Uso: User, Author, Librarian
using System;

namespace Biblioteca.Domain.ValueObjects;

public record class Email : ValueObject
{
    #region Propriedades
    public string EnderecoEmail { get; private set; }
    #endregion

    #region Construtores
    // Construtor da classe Email que recebe o endereço de e-mail e verifica se é válido.
    // Se o endereço for inválido, uma exceção (ArgumentException) é lançada. 
    public Email() { }
    public Email(string enderecoEmail)
    {
        if (string.IsNullOrWhiteSpace(enderecoEmail) || !enderecoEmail.Contains("@"))
            throw new ArgumentException("Endereço de e-mail inválido.", nameof(enderecoEmail));
        EnderecoEmail = enderecoEmail;
    }
    #endregion
}