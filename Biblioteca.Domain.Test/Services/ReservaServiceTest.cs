using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Services;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.Services;

public class ReservaServiceTest
{
    #region Métodos de fabrica
    private Leitor CriarLeitorValido()
    {
        var nome = new NomeCompleto("Lorena", "Lopes");
        var email = new Email("loriLopes@gmail.com");
        var cpf = new CPF("52998224725");
        var endereco = new Endereco(
            rua: "Rua Maria Adelia",
            numero: "160",
            bairro: "Centro",
            cidade: "São Paulo",
            estado: "SP",
            cep: "06514104",
            complemento: "Casa"
        );
        var dataCadastro = DateTime.Today;

        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }
    private Livro CriarLivroIndisponivel()
    {
        var autor = new Autor(new NomeCompleto("Autor", "T"), new Email("t@a.com"), new DateTime(1980, 1, 1));
        var livro = new Livro("Titulo", autor, new ISBN("1234567890"), 2000, 1, new List<Categoria> { new Categoria("Terror", Domain.Enums.ETipoCategoria.Terror) }, descricao: "Descrição do livro");
        livro.Emprestar();
        return livro;
    }
    #endregion
    #region  Testes Unitários
    [Fact]
    public void Deve_Criar_Reserva_Se_Livro_Nao_Disponivel()
    {
        //Arrange Prepare o cenário do teste (instancie objetos, defina valores).
        var leitor = CriarLeitorValido();
        var livro = CriarLivroIndisponivel();
        var service = new ReservaService();

        //Act Execute a ação que está sendo testada (chame o método ou função).
        var reserva = service.CriarReserva(leitor, livro);

        //Assert Verifique se o resultado está correto (use métodos como Assert.Equal, Assert.True etc).
        reserva.Should().NotBeNull();
        reserva.Livro.Should().Be(livro);
        reserva.Leitor.Should().Be(leitor);

    }
    [Fact]
    public void Nao_Deve_Reservar_Livro_Disponivel()
    {
        var leitor = CriarLeitorValido();
        var livro = CriarLivroIndisponivel();
        livro.Devolver();

        var service = new ReservaService();

        Action act = () => service.CriarReserva(leitor, livro);
        act.Should().Throw<InvalidOperationException>().WithMessage("Não é possível reservar um livro disponível.");
    }
    #endregion
}