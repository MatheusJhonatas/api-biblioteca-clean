using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Xunit;
using FluentAssertions;
using Biblioteca.Domain.ValueObjects;
namespace Biblioteca.Domain.Test.Entities;

public class EmprestimoTest
{
    #region Métodos de Fábrica

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
            cep: "06515033"
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
        var categorias = new List<Categoria> { new Categoria("Liderança", Biblioteca.Domain.Enums.ETipoCategoria.Lideranca) };
        return new Livro("A sorte segue a coragem.", autor, isbn, 2008, 23, categorias);
    }

    #endregion
    #region Testes Unitários
    [Fact]
    public void Deve_Criar_Emprestimo_Valido()
    {
        // Arrange é quando preparamos os dados necessários para o teste.
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        //Act é quando executamos a ação que queremos testar. aqui no caso é a criação do empréstimo.
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);
        // Assert é quando verificamos se o resultado é o esperado.
        emprestimo.Usuario.Should().Be(leitor);
        emprestimo.Livro.Should().Be(livro);
        emprestimo.DataEmprestimo.Should().Be(dataEmprestimo);
        emprestimo.DataPrevistaDevolucao.Should().Be(dataPrevista);
        emprestimo.Status.Should().Be(EStatusEmprestimo.Ativo);
        emprestimo.DataRealDevolucao.Should().BeNull();
    }

    [Fact]
    public void Nao_Deve_Criar_Emprestimo_Com_Leitor_Nulo()
    {
        // Arrange é quando preparamos os dados necessários para o teste.
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        //Act é quando executamos a ação que queremos testar. aqui no caso é a criação do empréstimo.
        // Estamos passando um leitor nulo, o que deve gerar uma exceção.
        Action act = () => new Emprestimo(null, livro, dataEmprestimo, dataPrevista);
        //Assert é quando verificamos se o resultado é o esperado.
        // Esperamos que uma exceção do tipo ArgumentNullException seja lançada, indicando que o parâmetro "usuario" é nulo.
        act.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == "usuario");
    }

    [Fact]
    public void Nao_Deve_Criar_Emprestimo_Com_Livro_Nulo()
    {
        var leitor = CriarLeitorValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        Action act = () => new Emprestimo(leitor, null, dataEmprestimo, dataPrevista);

        act.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == "livro");
    }

    [Fact]
    public void Deve_Finalizar_Emprestimo_Ativo()
    {
        // Arrange é quando preparamos os dados necessários para o teste.
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido(); var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);
        livro.Emprestar(); // Simulando que o livro está emprestado.
        // Act é quando executamos a ação que queremos testar. aqui no caso é a finalização do empréstimo.
        emprestimo.FinalizarEmprestimo(DateTime.Today.AddDays(8));
        // Assert é quando verificamos se o resultado é o esperado.
        emprestimo.Status.Should().Be(EStatusEmprestimo.Finalizado);
        emprestimo.DataRealDevolucao.Should().Be(DateTime.Today.AddDays(8));
        livro.Disponivel.Should().BeTrue();
    }

    [Fact]
    public void Nao_Deve_Finalizar_Emprestimo_Ja_Finalizado()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);
        livro.Emprestar(); // Simulando que o livro está emprestado.
        emprestimo.FinalizarEmprestimo(DateTime.Today.AddDays(8));

        Action act = () => emprestimo.FinalizarEmprestimo(DateTime.Today.AddDays(9));
        act.Should().Throw<InvalidOperationException>().WithMessage("Este empréstimo já foi finalizado.");
    }

    [Fact]
    public void Deve_Retornar_Emprestimo_Em_Andamento_Quando_Ativo_E_Sem_Devolucao()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = dataEmprestimo.AddDays(7);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);

        emprestimo.EmprestimoEmAndamento().Should().BeTrue();
    }

    [Fact]
    public void Deve_Retornar_EstaAtrasado_True_Quando_Apos_DataPrevista()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today.AddDays(-10);
        var dataPrevista = DateTime.Today.AddDays(-5);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);

        emprestimo.FinalizarEmprestimo(DateTime.Today);

        emprestimo.EstaAtrasado().Should().BeTrue();
    }

    [Fact]
    public void Deve_Calcular_Multa_Corretamente()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today.AddDays(-10);
        var dataPrevista = DateTime.Today.AddDays(-5);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);

        emprestimo.FinalizarEmprestimo(DateTime.Today);
        emprestimo.CalcularMulta().Should().Be(5.00m);
    }

    [Fact]
    public void Nao_Deve_Calcular_Multa_Se_Nao_Esta_Atrasado()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroValido();
        var dataEmprestimo = DateTime.Today;
        var dataPrevista = DateTime.Today.AddDays(5);
        var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);

        emprestimo.FinalizarEmprestimo(DateTime.Today);

        emprestimo.CalcularMulta().Should().Be(0.00m);
    }
    #endregion
}