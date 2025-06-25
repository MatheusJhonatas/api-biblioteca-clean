using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Test.ValueObjects;


public class NomeTest
{
    [Fact]
    public void ShouldOverrideToString()
    {
        // Arrange
        var nome = new Nome("João", "Silva");

        // Act
        var resultado = nome.ToString();

        // Assert
        Assert.Equal("João Silva", resultado);
    }
    [Fact]
    public void ShouldImplicitlyConvertToString()
    {
        // Arrange
        Nome nome = new Nome("Maria", "Oliveira");

        // Act
        string resultado = nome;

        // Assert
        Assert.Equal("Maria Oliveira", resultado);
    }
}