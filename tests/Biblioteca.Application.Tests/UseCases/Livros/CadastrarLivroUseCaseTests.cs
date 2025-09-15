
namespace Biblioteca.Application.Tests.UseCases.Livros;

public class CadastrarLivroUseCaseTests
{
    private readonly Mock<ILivroRepository> _livroRepositoryMock;
    private readonly Mock<IAutorRepository> _autorRepositoryMock;
    private readonly BibliotecarioService _bibliotecarioService;
    private readonly CadastrarLivroUseCase _useCase;
    public CadastrarLivroUseCaseTests()
    {
        _livroRepoMock = new Mock<ILivroRepository>();
        _autorRepoMock = new Mock<IAutorRepository>();
        _bibliotecarioService = new BibliotecarioService();
        _useCase = new CadastrarLivroUseCase(_livroRepoMock.Object, _autorRepoMock.Object, _bibliotecarioService);
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
    //metodo tem que ser async e retornar Task quando for testar metodos async
    [Fact]
    public async Task DeveRetornarErro_QuandoTituloForVazio()
    {
        // Arrange é quando você configura o cenário do teste
        var useCase = new CadastrarLivroUseCase(null, null, null);
        // Act é quando você executa a ação que está sendo testada
        var result = await useCase.Execute(new CadastrarLivroCommand { Titulo = "" });
        // Assert é quando você verifica se o resultado está correto
        Assert.False(result.IsSuccess);
        Assert.Equal("Título é obrigatório", result.Errors.First());
    }
}