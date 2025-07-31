using Xunit;
using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using Biblioteca.Domain.Services;
using FluentAssertions;
using System.ComponentModel.Design;

public class EmprestimoServiceTest
{
    #region Métodos de fabrica
    private Leitor CriarLeitorValido()
    {
        var nome = new NomeCompleto("Joao", "Victor");
        var email = new Email("joaovictor@gmail.com");
        var cpf = new CPF("52998224725");
        var endereco = new Endereco(
            rua: "Rua Dos Coqueiroos",
            numero: "120",
            bairro: "Centro",
            cidade: "São Paulo",
            estado: "SP",
            cep: "06515104"
        );
        var dataCadastro = DateTime.Today;

        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }
    private Livro CriarLivroValido()
    {

        var nomeCompleto = new NomeCompleto("Yunal Nooah", "Harari");
        var email = new Email("yunalharari@gmail.com");
        var dataNascimento = new DateTime(1989, 5, 30);
        var autor = new Autor(nomeCompleto, email, dataNascimento);
        var isbn = new ISBN("1234567890");
        var categorias = new List<Categoria> { new Categoria("Romance", Biblioteca.Domain.Enums.ETipoCategoria.Romance) };
        return new Livro("Sapiens.", autor, isbn, 2008, 23, categorias);
    }
    #endregion
    #region Testes Unitários
    [Fact]
    public void Deve_Realizar_Emprestimo_Com_Sucesso()
    {
        //Arrange Prepare o cenário do teste (instancie objetos, defina valores).
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var service = new EmprestimoService();

        //Act Execute a ação que está sendo testada (chame o método ou função).
        var emprestimo = service.RealizarEmprestimo(leitor, livro);
        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc);
        emprestimo.Should().NotBeNull();
        emprestimo.Livro.Should().Be(livro);
        emprestimo.Usuario.Should().Be(leitor);
        livro.Disponivel.Should().BeFalse();
    }
    [Fact]
    public void Nao_Deve_Realizar_Emprestimo_Se_Leitor_Estiver_Inadimplente()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var emprestimoService = new EmprestimoService();

        // Cria um empréstimo atrasado
        var emprestimoAtrasado = new Emprestimo(
            leitor,
            livro,
            DateTime.Now.AddDays(-10),
            DateTime.Now.AddDays(-5)
        );

        livro.Emprestar(); // importante: marca o livro como emprestado
        emprestimoAtrasado.FinalizarEmprestimo(DateTime.Now); // devolução atrasada

        leitor.AdicionarEmprestimo(emprestimoAtrasado); // leitor agora está inadimplente

        // Act
        Action act = () => emprestimoService.RealizarEmprestimo(leitor, livro);

        // Assert
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Leitor inadimplente não pode realizar empréstimos.");
    }
    #endregion
}