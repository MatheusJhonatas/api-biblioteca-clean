using Moq;
using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Requests.Autor;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.UseCases.Livros;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;


namespace Biblioteca.Application.Tests.UseCases.Livros;

public class CadastrarLivroUseCaseTests
{
    #region Arrange
    private readonly Mock<ILivroRepository> _livroRepositoryMock;
    private readonly Mock<IAutorRepository> _autorRepositoryMock;
    private readonly BibliotecarioService _bibliotecarioService;
    private readonly CadastrarLivroUseCase _useCase;
    public CadastrarLivroUseCaseTests()
    {
        _livroRepositoryMock = new Mock<ILivroRepository>();
        _autorRepositoryMock = new Mock<IAutorRepository>();
        _bibliotecarioService = new BibliotecarioService();
        _useCase = new CadastrarLivroUseCase(_livroRepositoryMock.Object, _autorRepositoryMock.Object, _bibliotecarioService);
    }
    private CadastrarLivroRequest CriarRequestValido()
    {
        return new CadastrarLivroRequest
        {
            Titulo = "Sapiens",
            Autor = new AutorRequest
            {
                NomeCompleto = new NomeCompletoRequest { PrimeiroNome = "Yuval", UltimoNome = "Harari" },
                Email = new EmailRequest { EnderecoEmail = "yuval@gmail.com" },
                DataNascimento = new DateTime(1976, 2, 24)
            },
            ISBN = "9788535925699",
            AnoPublicacao = 2011,
            NumeroPaginas = 498,
            Descricao = "Uma breve história da humanidade",
            Categorias = new List<CategoriaRequest>
                {
                    new CategoriaRequest { Nome = "História", Tipo = (int)ETipoCategoria.Historia }
                }
        };
    }
    #endregion
    #region Tests
    //metodo tem que ser async e retornar Task quando for testar metodos async
    [Fact]
    public async Task Deve_Falhar_Se_Titulo_For_Vazio()
    {
        // Arrange É quando você configura o cenário do teste
        var request = CriarRequestValido();
        request.Titulo = "";

        // Act é quando você executa a ação que está sendo testada
        var result = await _useCase.Execute(request);

        // Assert é quando você verifica se o resultado está correto
        Assert.False(result.Success);
        Assert.Equal("O título do livro é obrigatório.", result.Message);
    }
    [Fact]
    public async Task Se_Data_Nascimento_Do_Autor_For_Futura_Deve_Falhar()
    {
        // Arrange é quando você configura o cenário do teste.
        var request = CriarRequestValido();
        request.Autor.DataNascimento = DateTime.Today.AddDays(1); // Data futura
        // Act é quando você executa a ação que está sendo testada.
        var result = await _useCase.Execute(request);
        // Assert é quando você verifica se o resultado está correto.
        Assert.False(result.Success);
        Assert.Equal("A data de nascimento não pode ser futura.", result.Message);
    }
    [Fact]
    public async Task Se_Data_Nascimento_Do_Autor_For_Default_Deve_Falhar()
    {
        // Arrange é quando você configura o cenário do teste.
        var request = CriarRequestValido();
        request.Autor.DataNascimento = default; // Data inválida
        // Act é quando você executa a ação que está sendo testada.
        var result = await _useCase.Execute(request);
        // Assert é quando você verifica se o resultado está correto.
        Assert.False(result.Success);
        Assert.Equal("A data de nascimento do autor é obrigatória e deve ser válida.", result.Message);
    }
    [Fact]
    public async Task Se_Numero_De_Paginas_For_Menor_Ou_Igual_A_Zero_Deve_Falhar()
    {
        //Arrange é quando você configura o cenário do teste.
        var request = CriarRequestValido();
        request.NumeroPaginas = 0; // Número de páginas inválido
        //Act é quando você executa a ação que está sendo testada.
        var result = await _useCase.Execute(request);
        //Assert é quando você verifica se o resultado está correto.
        Assert.False(result.Success);
        Assert.Equal("O número de páginas deve ser maior que zero.", result.Message);
    }
    public async Task Se_Autor_For_Nulo_Deve_Falhar()
    {
        //Arrange é quando você configura o cenário do teste.
        var request = CriarRequestValido();
        request.Autor = null; // Autor nulo
        //Act é quando você executa a ação que está sendo testada.
        var result = await _useCase.Execute(request);
        //Assert é quando você verifica se o resultado está correto.
        Assert.False(result.Success);
        Assert.Equal("As informações do autor são obrigatórias.", result.Message);
    }
    [Fact]
    public async Task Se_ISBN_For_Vazio_Deve_Falhar()
    {
        //Arrange é quando você configura o cenário do teste.
        var request = CriarRequestValido();
        request.ISBN = ""; // ISBN vazio
        //Act é quando você executa a ação que está sendo testada.
        var result = await _useCase.Execute(request);
        //Assert é quando você verifica se o resultado está correto.
        Assert.False(result.Success);
        Assert.Equal("O ISBN é obrigatório.", result.Message);
    }

    #endregion
}
