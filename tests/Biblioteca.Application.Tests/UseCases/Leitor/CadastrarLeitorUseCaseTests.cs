using Biblioteca.Domain.Interfaces;
using Moq;

namespace Biblioteca.Application.Tests.UseCases.Leitor;

public class CadastrarLeitorUseCaseTests
{
    private readonly Mock<ILeitorRepository> _leitorRepositoryMock;

    public CadastrarLeitorUseCaseTests()
    {
        _leitorRepositoryMock = new Mock<ILeitorRepository>();
    }
}