using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
namespace Biblioteca.Domain.Test.ValueObjects;

    public class GeneroTests
    {
        [Fact]
        public void CriarGenero_ComNomeValido_DeveCriarComSucesso()
        {
            // Arrange
            var nome = "Ficção";

            // Act
            var genero = new Genero(nome);

            // Assert
            genero.Nome.Should().Be(nome);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void CriarGenero_ComNomeVazioOuNulo_DeveLancarExcecao(string nomeInvalido)
        {
            // Act
            Action act = () => new Genero(nomeInvalido);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .WithMessage("O gênero não pode ser vazio.");
        }

        [Fact]
        public void CriarGenero_ComNomeInvalido_DeveLancarExcecao()
        {
            // Arrange
            var nomeInvalido = "Aventura"; // Não está na lista

            // Act
            Action act = () => new Genero(nomeInvalido);

            // Assert
            act.Should()
               .Throw<ArgumentException>()
               .WithMessage($"O gênero '{nomeInvalido}' não é válido.");
        }

        [Fact]
        public void ToString_DeveRetornarNomeDoGenero()
        {
            // Arrange
            var genero = new Genero("Fantasia");

            // Act
            var resultado = genero.ToString();

            // Assert
            resultado.Should().Be("Fantasia");
        }

        [Fact]
        public void DoisGeneros_ComMesmoNome_DevemSerIguais()
        {
            // Arrange
            var genero1 = new Genero("Romance");
            var genero2 = new Genero("Romance");

            // Act & Assert
            genero1.Should().Be(genero2);
            genero1.Equals(genero2).Should().BeTrue();
            genero1.GetHashCode().Should().Be(genero2.GetHashCode());
        }

        [Fact]
        public void DoisGeneros_ComNomesDiferentes_NaoDevemSerIguais()
        {
            // Arrange
            var genero1 = new Genero("Romance");
            var genero2 = new Genero("Terror");

            // Act & Assert
            genero1.Should().NotBe(genero2);
            genero1.Equals(genero2).Should().BeFalse();
        }
    }

