using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblioteca.Application.UseCases.Emprestimos;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using Xunit;

namespace Biblioteca.tests.Application.UseCases.Emprestimos
{
    public class ListarEmprestimosUseCaseTests
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
        public async Task ListarEmprestimos_QuandoChamado_DeveRetornarListaEmprestimos()
        {
            // Arrange
            var leitor = CriarLeitorValido();
            var livro = CriarLivroValido();
            var emprestimo = new Emprestimo(leitor, livro, DateTime.Today, DateTime.Today.AddDays(7));

            var mockRepo = new Mock<IEmprestimoRepository>();
            mockRepo.Setup(r => r.ListarTodosAsync()).ReturnsAsync(new List<Emprestimo> { emprestimo });

            var useCase = new ListarEmprestimoUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Empréstimos listados com sucesso.");
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCount(1);
            result.Data.First().LivroTitulo.Should().Be("A sorte segue a coragem.");
            mockRepo.Verify(r => r.ListarTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ListarEmprestimos_QuandoRepositorioLancaExcecao_DeveRetornarFalha()
        {
            // Arrange
            var mockRepo = new Mock<IEmprestimoRepository>();
            mockRepo.Setup(r => r.ListarTodosAsync()).ThrowsAsync(new Exception("Banco indisponível"));

            var useCase = new ListarEmprestimoUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync();

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Contain("Erro ao listar empréstimos");
            result.Data.Should().BeNull();
            mockRepo.Verify(r => r.ListarTodosAsync(), Times.Once);
        }
    }
}