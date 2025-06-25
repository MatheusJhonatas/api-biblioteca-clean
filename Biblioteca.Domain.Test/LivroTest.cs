using Biblioteca.Domain.Entities;

namespace Biblioteca.DomainTest;

public class LivroTest
{
    [Fact]
    public void Test1()
    {
        var livro = new Livro(
            "O Senhor dos Anéis",
            "J.R.R. Tolkien",
            "Fantasia",
            1954,
            true
        );
        
        
    }
}