using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.Entites;

public class BibliotecarioTest
{
    #region Metodos de Facbrica
    private Bibliotecario CriarBibliotecarioValido()
    {
        var nome = new NomeCompleto("Henrique", "Silva");
        var email = new Email("henriquesilva@hotmail.com");
        var matricula = "HEN789";
        var cargo = "Supervisor";
        return new Bibliotecario(nome, email, matricula, cargo);
    }
    #endregion
    #region Testes Unitários
    [Fact]
    public void Dado_Bibliotecario_Valido_Deve_Possuir_Dados_Corretos()
    {
        //Arrange
        var bibliotecario = CriarBibliotecarioValido();
        //Act Execute a ação que está sendo testada (chame o método ou função).
        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc);
        bibliotecario.NomeCompleto.PrimeiroNome.Should().Be("Henrique");
        bibliotecario.NomeCompleto.UltimoNome.Should().Be("Silva");
        bibliotecario.Email.EnderecoEmail.Should().Be("henriquesilva@hotmail.com");
        bibliotecario.Matricula.Should().Be("HEN789");
        bibliotecario.Cargo.Should().Be("Supervisor");
    }
    [Fact]
    public void Nao_Deve_Criar_Bibliotecario_Com_Nome_Nulo()
    {
        //Arrange
        var email = new Email("henriquesilva@hotmail.com");
        var matricula = "HEN789";
        var cargo = "Administrador";
        //Act
        Action act = () => new Bibliotecario(null, email, matricula, cargo);
        //Assert
        act.Should().Throw<ArgumentNullException>()
       .Where(e => e.ParamName == "nomeCompleto");
    }
    [Fact]
    public void Nao_Deve_Criar_Bibliotecario_Com_Email_Nulo()
    {
        //Arrange
        var nome = new NomeCompleto("Helio", "Sebastião");
        var matricula = "HEN789";
        var cargo = "Auxiliar";
        //Act
        Action act = () => new Bibliotecario(nome, null, matricula, cargo);
        //Assert
        act.Should().Throw<ArgumentNullException>()
       .Where(e => e.ParamName == "email");
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Nao_Deve_Criar_Bibliotecario_Com_Matricula_Invalida(string matriculaInvalida)
    {
        // Arrange
        var nome = new NomeCompleto("Clara", "Lima");
        var email = new Email("clara.lima@teste.com");
        var cargo = "Coordenadora";

        // Act
        Action act = () => new Bibliotecario(nome, email, matriculaInvalida, cargo);

        // Assert
        act.Should().Throw<ArgumentException>()
       .WithMessage("Matrícula inválida*")
       .Where(e => e.ParamName == "matricula");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Nao_Deve_Criar_Bibliotecario_Com_Cargo_Invalido(string cargoInvalido)
    {
        // Arrange
        var nome = new NomeCompleto("Carlos", "Ferreira");
        var email = new Email("carlos@teste.com");
        var matricula = "MAT999";

        // Act
        Action act = () => new Bibliotecario(nome, email, matricula, cargoInvalido);

        // Assert
        act.Should().Throw<ArgumentException>()
      .WithMessage("Cargo inválido*")
      .Where(e => e.ParamName == "cargo");
    }

    #endregion
}