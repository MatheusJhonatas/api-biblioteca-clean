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
    #endregion
}