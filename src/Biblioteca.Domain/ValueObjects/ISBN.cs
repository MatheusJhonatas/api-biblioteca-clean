// Atributos: Código ISBN
// Regras: Deve ter 10 ou 13 dígitos e seguir o padrão internacional
// Uso: Book
using System;

namespace Biblioteca.Domain.ValueObjects;

public record class ISBN : ValueObject
{
    #region Propriedades
    public string Codigo { get; private set; }
    #endregion

    #region Construtores
    public ISBN() { }
    public ISBN(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo) || (codigo.Length != 10 && codigo.Length != 13))
        {
            throw new ArgumentException("O código ISBN deve ter 10 ou 13 dígitos.");
        }

        Codigo = codigo;
    }
    #endregion

    // Regras: O código ISBN deve ser único para cada livro.
}