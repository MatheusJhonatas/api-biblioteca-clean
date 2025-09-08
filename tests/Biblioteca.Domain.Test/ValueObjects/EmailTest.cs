using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.ValueObjects;

public class EmailTest
{
    #region Testes Unitários
    [Fact]
    public void Deve_Criar_Email_Valido()
    {
        // Arrange
        var email = new Email("teste@teste.com");
        // Act & Assert
        email.Should().NotBeNull();
        email.EnderecoEmail.Should().Be("teste@teste.com");
    }
    #endregion
}