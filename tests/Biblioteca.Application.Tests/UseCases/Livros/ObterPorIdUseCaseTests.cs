using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Domain.Interfaces;
using Moq;

namespace Biblioteca.Application.Test.UseCases.Livros
{
    public class ObterLivroPorIdUseCaseTest
    {
        private readonly Mock<ILivroRepository> _livroRepoMock;
        private readonly ObterLivroPorIdUseCase _useCase;
        public ObterLivroPorIdUseCaseTest()
        {
            _livroRepoMock = new Mock<ILivroRepository>();
            _useCase = new ObterLivroPorIdUseCase(_livroRepoMock.Object);
        }
        [Fact]
        public async Task Se_Livro_for_Null_Retorna_Fail()
        {
            // Arrange
            var livroId = Guid.NewGuid();
            _livroRepoMock.Setup(repo => repo.ObterPorIdAsync(livroId))
                          .ReturnsAsync((Biblioteca.Domain.Entities.Livro?)null);

            // Act
            var result = await _useCase.ExecuteAsync(livroId);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Livro não encontrado, verifique o id digitado..", result.Message);
        }
    }
}