using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Test.ValueObjects;


public class NomeTest
{
    private readonly Nome _nome = new Nome("João", "Silva");
    [Fact]
    public void ShouldOverrideToString()
    {
        // // Arrange
        // Nome nome = new Nome("João", "Silva");
        // Act
        var resultado = _nome.ToString();

        // Assert
        Assert.Equal("João Silva", resultado);
    }
    [Fact]
    public void ShouldImplicitlyConvertToString()
    {
        // Arrange
        // Nome nome = new Nome("Maria", "Oliveira");

        // Act
        string resultado = _nome;

        // Assert
        Assert.Equal("Maria Oliveira", resultado);
    }
}