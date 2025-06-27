using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.DomainTest;

public class LivroTest
{
    [Fact]
    public void Test1()
    {
        var livro = new Livro(
            "O Senhor dos Anéis",
            "J.R.R. Tolkien",
            new Genero("Fantasia"),
            1954,
            true
        );
    }
}