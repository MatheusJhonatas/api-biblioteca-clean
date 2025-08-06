using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.ValueObjects;

public class CpfTest
{
    #region Testes Unitários
    [Fact]
    public void Deve_Criar_Cpf_Valido()
    {
        // Arrange
        var cpf = new CPF("52998224725");

        // Act & Assert
        cpf.Should().NotBeNull();
        cpf.Numero.Should().Be("52998224725");
    }

    [Fact]
    public void Nao_Deve_Criar_Cpf_Invalido()
    {
        // Arrange
        Action action = () => new CPF("12345678901");

        // Act & Assert
        action.Should().Throw<ArgumentException>().WithMessage("CPF inválido*");
    }
    [Fact]
    public void Nao_Deve_Criar_Cpf_Com_Numero_Nulo()
    {
        // Arrange
        Action action = () => new CPF(null);

        // Act & Assert
        action.Should().Throw<ArgumentException>().WithMessage("CPF não pode ser nulo ou vazio*")
            .And.ParamName.Should().Be("numero");
    }
    [Fact]
    public void Nao_Deve_Criar_Cpf_Com_Numero_Vazio()
    {
        // Arrange
        Action action = () => new CPF("");

        // Act & Assert
        action.Should().Throw<ArgumentException>().WithMessage("CPF não pode ser nulo ou vazio*")
            .And.ParamName.Should().Be("numero");
    }
    #endregion
}