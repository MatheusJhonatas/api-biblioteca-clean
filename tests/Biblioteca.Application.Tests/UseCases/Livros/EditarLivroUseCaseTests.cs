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
}