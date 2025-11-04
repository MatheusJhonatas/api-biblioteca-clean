
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.ValueObjects;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Domain.Services;

using FluentAssertions;
using Moq;
using Xunit;

namespace Biblioteca.Application.Tests.UseCases.Emprestimos.Leitores
{
    public class EmprestarLivroUseCaseTests
    {
        #region Métodos de Fábrica

        private static Biblioteca.Domain.Entities.Leitor CriarLeitorValido()
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

            return new Biblioteca.Domain.Entities.Leitor(nome, email, cpf, endereco, dataCadastro);
        }

        private static Livro CriarLivroValido()
        {
            var nomeCompleto = new NomeCompleto("Sun", "Tzu");
            var email = new Email("suntzu@gmail.com");
            var dataNascimento = new DateTime(1964, 3, 14);
            var autor = new Autor(nomeCompleto, email, dataNascimento);
            var isbn = new ISBN("1234567890");
            var categorias = new List<Categoria> { new Categoria("Liderança", ETipoCategoria.Lideranca) };

            return new Livro("A sorte segue a coragem.", autor, isbn, 2008, 23, categorias, "Um livro sobre coragem e determinação.");
        }

        #endregion

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
            mockEmprestimoRepo.Setup(r => r.SalvarAsync(It.IsAny<Emprestimo>())).Returns(Task.CompletedTask);

            var mockEmprestimoService = new Mock<EmprestimoService>();

            var useCase = new EmprestarLivroUseCase(
                mockLeitorRepo.Object,
                mockLivroRepo.Object,
                mockEmprestimoRepo.Object,
                mockEmprestimoService.Object
            );

            var leitorId = Guid.NewGuid();
            var livroId = Guid.NewGuid();
            var dataEmprestimo = DateTime.Today;
            var dataDevolucao = DateTime.Today.AddDays(7);
            var request = new EmprestarLivroRequest(leitorId, livroId);

            // Act
            var result = await useCase.ExecuteAsync(request);

            // Assert
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Empréstimo realizado com sucesso.");

            mockLeitorRepo.Verify(r => r.ObterPorIdAsync(leitorId), Times.Once);
            mockLivroRepo.Verify(r => r.ObterPorIdAsync(livroId), Times.Once);
            mockEmprestimoRepo.Verify(r => r.SalvarAsync(It.IsAny<Emprestimo>()), Times.Once);
        }
    }
}
