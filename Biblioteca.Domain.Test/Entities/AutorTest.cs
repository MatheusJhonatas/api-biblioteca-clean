using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.Entites;

public class AutorTest
{
    #region Metodos de Fábricas
    private Autor CriarAutorValido()
    {
        var nome = new NomeCompleto("Jk", "Rolling");
        var email = new Email("jkrolling@gmail.com");
        var nascimento = new DateTime(1978, 12, 10);

        return new Autor(nome, email, nascimento);
    }
    #endregion
    #region Testes Unitários
    [Fact]
    public void Dado_Um_Autor_Valido_Deve_Possuir_Dados_Corretos()
    {
        //Arrange Prepare o cenário do teste (instancie objetos, defina valores).
        var autor = CriarAutorValido();
        //Act Execute a ação que está sendo testada (chame o método ou função).
        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc);
        autor.NomeCompleto.PrimeiroNome.Should().Be("Jk");
        autor.NomeCompleto.UltimoNome.Should().Be("Rolling");
        autor.Email.EnderecoEmail.Should().Be("jkrolling@gmail.com");
        autor.DataNascimento.Should().Be(new DateTime(1978, 12, 10));
    }
    [Theory]//Essa anotação do xUnit indica que este teste será executado múltiplas vezes, com diferentes valores de entrada.
    [InlineData(null, "Rolling")]
    [InlineData("JK", null)]
    [InlineData("", "Rolling")]
    [InlineData("JK", "")]
    public void Nao_Deve_Criar_Autor_Com_Nome_Valido(string primeiroNome, string ultimoNome)
    {
        // Arrange, Aqui começa a preparação dos dados para o teste.O e-mail e a data de nascimento são válidos (para garantir que a falha virá do nome inválido e não de outro campo).
        var email = new Email("jkrolling@gmail.com");
        var nascimento = new DateTime(1978, 12, 10);
        // Act
        Action act = () =>
        {
            var nome = new NomeCompleto(primeiroNome, ultimoNome);
            var autor = new Autor(nome, email, nascimento);
        };
        // Assert
        act.Should().Throw<ArgumentException>();
    }
    #endregion
}