public class CadastrarLivroUseCaseTests
{
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