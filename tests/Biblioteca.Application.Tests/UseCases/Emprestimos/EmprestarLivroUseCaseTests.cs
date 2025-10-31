using Biblioteca.Domain.ValueObjects;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Moq;
namespace Biblioteca.Application.Tests.UseCases.Emprestimos;

public class EmprestarLivroUseCaseTests
{
    private static Leitor CriarLeitorValido()
    {
        var nome = new NomeCompleto("Sun", "Tzu");
        var email = new Email("suntzu@gmail.com");
        var cpf = new CPF("52998224725");
        var endereco = new Endereco(
            rua: "Rua Uruguai",
            numero: "122",
            bairro: "Centro",
            cidade: "São Paulo",
            estado: "SP",
            cep: "06515033",
            complemento: "Apto 303"
        );
        var dataCadastro = DateTime.Today;

        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }

    private static Livro CriarLivroValido()
    {
        var nomeCompleto = new NomeCompleto("Sun", "Tzu");
        var email = new Email("suntzu@gmail.com");
        var dataNascimento = new DateTime(1964, 3, 14);
        var autor = new Autor(nomeCompleto, email, dataNascimento);
        var isbn = new ISBN("1234567890");
        var categorias = new List<Categoria> { new Categoria("Liderança", ETipoCategoria.Lideranca) };
        return new Livro("A sorte segue a coragem.", autor, isbn, 2008, 23, categorias, descricao: "Um livro sobre coragem e determinação.");
    }
    [Fact]
    public async Task EmprestarLivro_QuandoDadosSaoValidos_DeveRetornarSucesso()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();


        var mockLeitorRepo = new Mock<ILeitorRepository>();
        mockLeitorRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(leitor);

        var mockLivroRepo = new Mock<ILivroRepository>();
        mockLivroRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(livro);

        var mockEmprestimoRepo = new Mock<IEmprestimoRepository>();
        mockEmprestimoRepo.Setup(r => r.AtualizarAsync(It.IsAny<Emprestimo>())).Returns(Task.CompletedTask);

        var useCase = new EmprestarLivroUseCase(mockLeitorRepo.Object, mockLivroRepo.Object, mockEmprestimoRepo.Object);

        var leitorId = Guid.NewGuid();
        var livroId = Guid.NewGuid();
        var dataEmprestimo = DateTime.Today;
        var dataDevolucao = DateTime.Today.AddDays(7);

        // Act
        var result = await useCase.ExecuteAsync(leitorId, livroId, dataEmprestimo, dataDevolucao);

        // Assert
        result.Success.Should().BeTrue();
        result.Message.Should().Be("Empréstimo realizado com sucesso.");
        mockLeitorRepo.Verify(r => r.ObterPorIdAsync(leitorId), Times.Once);
        mockLivroRepo.Verify(r => r.ObterPorIdAsync(livroId), Times.Once);
        mockEmprestimoRepo.Verify(r => r.AtualizarAsync(It.IsAny<Emprestimo>()), Times.Once);
    }
}