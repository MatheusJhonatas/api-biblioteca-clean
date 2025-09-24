using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Leitor;
using Biblioteca.Application.UseCases.Leitores;
using Biblioteca.Domain.Interfaces;
using Moq;

namespace Biblioteca.Application.Tests.UseCases.Leitor;

public class CadastrarLeitorUseCaseTests
{
    private readonly Mock<ILeitorRepository> _leitorRepositoryMock;
    private readonly CadastrarLeitorUseCase _cadastrarLeitorUseCase;


    public CadastrarLeitorUseCaseTests()
    {
        _leitorRepositoryMock = new Mock<ILeitorRepository>();
        _cadastrarLeitorUseCase = new CadastrarLeitorUseCase(_leitorRepositoryMock.Object);

    }
    private CadastrarLeitorRequest CriarRequestValido()
    {
        //criandoo request valido para os testes, para evitar repetição de código
        return new CadastrarLeitorRequest
        {
            NomeCompleto = new NomeCompletoRequest { PrimeiroNome = "Ana", UltimoNome = "Silva" },
            Email = new EmailRequest { EnderecoEmail = "ana@teste.com" },
            Cpf = new CpfRequest { NumeroCpf = "52998224725" },
            Endereco = new EnderecoRequest
            {
                Rua = "Rua A",
                Numero = "123",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                Cep = "06514104",
                Complemento = "Apto 1"
            },
            DataCadastro = DateTime.Today
        };
    }
    [Fact]
    public async Task Deve_Cadastrar_Leitor_Com_Sucesso()
    {
        // Arrange
        var request = CriarRequestValido();
        var repoMock = new Mock<ILeitorRepository>();
        repoMock.Setup(r => r.SalvarAsync(It.IsAny<Biblioteca.Domain.Entities.Leitor>())).Returns(Task.CompletedTask);
        var useCase = new CadastrarLeitorUseCase(repoMock.Object);

        // Act
        var result = await useCase.Execute(request);

        // Assert
        Assert.True(result.Success);
        repoMock.Verify(r => r.SalvarAsync(It.IsAny<Biblioteca.Domain.Entities.Leitor>()), Times.Once);

    }
    [Fact]
    public async Task Deve_Retornar_Falha_Quando_Request_For_Nulo()
    {
        // Arrange
        CadastrarLeitorRequest request = null;

        // Act
        var result = await _cadastrarLeitorUseCase.Execute(request);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("Os dados do leitor são obrigatórios.", result.Message);
        _leitorRepositoryMock.Verify(r => r.SalvarAsync(It.IsAny<Biblioteca.Domain.Entities.Leitor>()), Times.Never);
    }
}