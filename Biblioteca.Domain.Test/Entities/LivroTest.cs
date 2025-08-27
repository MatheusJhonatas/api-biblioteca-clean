using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;
namespace Biblioteca.Domain.Test.Entites;

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
        var categorias = new List<Categoria> { new Categoria("Romance", Enums.ETipoCategoria.Romance) };
        return new Livro("A sorte segue a coragem.", autor, isbn, 2008, 23, categorias, descricao: "Um livro sobre coragem e determinação.");
    }
    #endregion
    #region Testes Unitários
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
    [Fact]//O atributo [Fact] vem do framework de testes xUnit. Ele indica que o método abaixo é um teste unitário que deve ser executado pelo runner de testes. Não recebe parâmetros e é usado para testes simples.
    public void Dado_Um_Livro_Disponivel_Deve_Emprestar_ComSucesso()
    {
        // Arrange Prepare o cenário do teste (instancie objetos, defina valores).
        var livro = Criar_Livro_Valido();

        //Act Execute a ação que está sendo testada (chame o método ou função).
        livro.Emprestar();

        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc)
        livro.Disponivel.Should().BeFalse();
    }
    [Fact]
    public void Dado_Um_LivroEmprestado_Deve_DevolverComsucesso()
    {
        //Arrange Prepare o cenário do teste (instancie objetos, defina valores).
        var livro = Criar_Livro_Valido();
        livro.Emprestar();
        //Act Execute a ação que está sendo testada (chame o método ou função).
        livro.Devolver();
        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc)
        livro.Disponivel.Should().BeTrue();
    }
    [Fact]
    public void Dado_Um_LivroJaDisponivel_Deve_LancarExecaoAoDevolver()
    {
        //Assert
        var livro = Criar_Livro_Valido();
        //Act
        Action act = () => livro.Devolver();
        //Assert
        act.Should().Throw<InvalidOperationException>()
        .WithMessage("O livro já está disponível.");
    }
    [Fact]
    public void Deve_VerificarDisponibilidade_Corretamente()
    {
        //Arrange
        var livro = Criar_Livro_Valido();
        //Act, Assert
        livro.VerificaDisponibilidade().Should().BeTrue();

        livro.Emprestar();
        livro.VerificaDisponibilidade().Should().BeFalse();

    }
    #endregion
}