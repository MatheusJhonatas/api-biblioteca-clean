using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Domain.Interfaces;
using Moq;

namespace Biblioteca.Application.Tests.UseCases.Livros;

public class EditarLivroUseCaseTests
{
    // Implementar testes unitários para EditarLivroUseCase
    private readonly Mock<ILivroRepository> _livroRepositoryMock;
    private readonly EditarLivroUseCase _editarLivroUseCase;
    public EditarLivroUseCaseTests()
    {
        _livroRepositoryMock = new Mock<ILivroRepository>();
        _editarLivroUseCase = new EditarLivroUseCase(_livroRepositoryMock.Object);
    }
    private EditarLivroRequest CriarRequestValido()
    {
        return new EditarLivroRequest(
            "Novo Título",
            2022,
            350
        );
    }
    // Teste para verificar se o livro é atualizado corretamente
    [Fact]
    public async Task Deve_Atualizar_Livro_Corretamente()
    {
        // Arrange
        var livroId = Guid.NewGuid();
        var request = CriarRequestValido();
        var email = new Biblioteca.Domain.ValueObjects.Email("autor@email.com"); // criando um email válido
        var nomeCompleto = new Biblioteca.Domain.ValueObjects.NomeCompleto("Nome", "do Autor"); // criando um nome válido dado que é um value object
        var autor = new Biblioteca.Domain.Entities.Autor(nomeCompleto, email, DateTime.Now); // ajustando a data de nascimento
        var isbn = new Biblioteca.Domain.ValueObjects.ISBN("1234567890123"); // criando um ISBN válido
        var categorias = new List<Biblioteca.Domain.Entities.Categoria>(); // adicionando lista vazia de categorias
        var livroExistente = new Biblioteca.Domain.Entities.Livro(
            "Título Antigo",
            autor,
            isbn,
            2020,
            300,
            categorias,
            null // assumindo que a capa é opcional
        );

        _livroRepositoryMock.Setup(repo => repo.ObterPorIdAsync(livroId))
            .ReturnsAsync(livroExistente);

        // Act
        await _editarLivroUseCase.ExecuteAsync(livroId, request);

        // Assert
        _livroRepositoryMock.Verify(repo => repo.AtualizarAsync(It.Is<Biblioteca.Domain.Entities.Livro>(l =>
            l.Titulo == request.NovoTitulo &&
            l.AnoPublicacao == request.NovoAnoPublicacao &&
            l.NumeroPaginas == request.NovoNumeroPaginas
        )), Times.Once);
    }
}