using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
namespace Biblioteca.Domain.Test;

public class LivroTest
{
    #region Factory methods
    private Livro Criar_Livro_Valido()
    {
        var nomeCompleto = new NomeCompleto("Mario Sergio", "Cortella");
        var email = new Email("marioSergioCortella@gmail.com");
        var dataNascimento = new DateTime(1964, 3, 14);
        var autor = new Autor(nomeCompleto, email, dataNascimento);
        var isbn = new ISBN("1234567890");
        var categorias = new List<Categoria> { new Categoria(Enums.ETipoCategoria.Romance) };
        return new Livro("A sorte segue a coragem.", autor, isbn, 2008, 23, categorias);
    }

    private Livro Criar_Livro_Invalido()
    {
        // Exemplo: título vazio, autor nulo, ISBN inválido, ano negativo, páginas zero, categorias vazias
        var isbn = new ISBN(""); // ISBN inválido
        return new Livro(
            "", // título inválido
            null, // autor nulo
            isbn,
            -1, // ano inválido
            0, // páginas inválidas
            new List<Categoria>() // categorias vazias
        );
    }
    #endregion
    [Fact]
    public void Dado_Um_LivroIndisponivel_Deve_LancarExcecaoAoEmprestar()
    {
        //Arrange
        var livro = Criar_Livro_Valido();
        livro.Emprestar();//agora está indisponivel

        //Act
        Action act = () => livro.Emprestar();

        //Assert
        act.Should().Throw<InvalidOperationException>()
        .WithMessage("O livro já está emprestado.");
    }
    [Fact]
    public void Dado_Um_Livro_Disponivel_Deve_Emprestar_ComSucesso()
    {
        // Arrange
        var livro = Criar_Livro_Valido();

        //Act
        livro.Emprestar();

        //Assert
        livro.Disponivel.Should().BeFalse();
    }
}