using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Xunit;
using FluentAssertions;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Test.Entities;

public class ReservaTest
{
    #region Métodos de Fábrica
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
            cep: "06514104"
        );
        var dataCadastro = DateTime.Today;

        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }

    private Livro CriarLivroEmprestado()
    {
        var autor = new Autor(new NomeCompleto("Autor", "T"), new Email("t@a.com"), new DateTime(1980, 1, 1));
        var livro = new Livro("Titulo", autor, new ISBN("1234567890"), 2000, 1, new List<Categoria> { new Categoria("Terror", ETipoCategoria.Terror) });
        livro.Emprestar();
        return livro;
    }
    #endregion
    #region Testes Unitários
    [Fact]
    public void Deve_Criar_Reserva_Ativa_Valida()
    {
        // Arrange: preparar os dados necessários
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();

        // Act: executar a ação a ser testada
        var reserva = new Reserva(leitor, livro);

        // Assert: verificar o resultado esperado
        reserva.Usuario.Should().Be(leitor);
        reserva.Livro.Should().Be(livro);
        reserva.Status.Should().Be(EStatusReserva.Ativa);
        reserva.DataReserva.Date.Should().Be(DateTime.Now.Date);
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Leitor_Nulo()
    {
        // Arrange
        var livro = CriarLivroEmprestado();

        // Act
        Action act = () => new Reserva(null, livro);

        // Assert
        act.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == "usuario");
    }

    [Fact]
    public void Nao_Deve_Criar_Reserva_Com_Livro_Nulo()
    {
        // Arrange
        var leitor = CriarLeitorValido();

        // Act
        Action act = () => new Reserva(leitor, null);

        // Assert
        act.Should().Throw<ArgumentNullException>().Where(e => e.ParamName == "livro");
    }

    [Fact]
    public void Deve_Cancelar_Reserva_Ativa()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);

        // Act
        reserva.Cancelar();

        // Assert
        reserva.Status.Should().Be(EStatusReserva.Cancelada);
    }

    [Fact]
    public void Nao_Deve_Cancelar_Reserva_Ja_Cancelada_Ou_Atendida()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);
        reserva.Cancelar();

        // Act
        Action act = () => reserva.Cancelar();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("Reserva já cancelada ou atendida.");
    }

    [Fact]
    public void Deve_Marcar_Reserva_Como_Atendida()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);

        // Act
        reserva.MarcarComoAtendida();

        // Assert
        reserva.Status.Should().Be(EStatusReserva.Atendida);
    }

    [Fact]
    public void Nao_Deve_Marcar_Reserva_Como_Atendida_Se_Ja_Finalizada()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);
        reserva.MarcarComoAtendida();

        // Act
        Action act = () => reserva.MarcarComoAtendida();

        // Assert
        act.Should().Throw<InvalidOperationException>().WithMessage("A reserva já foi finalizada.");
    }

    [Fact]
    public void Deve_Verificar_Se_Reserva_Esta_Valida()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);

        // Act
        var estaValida = reserva.EstaValida();

        // Assert
        estaValida.Should().BeTrue();
    }

    [Fact]
    public void Nao_Deve_Estar_Valida_Se_Reserva_Expirou()
    {
        // Arrange
        var leitor = CriarLeitorValido();
        var livro = CriarLivroEmprestado();
        var reserva = new Reserva(leitor, livro);

        // Manipula a data para simular expiração
        typeof(Reserva)
            .GetProperty("DataReserva")!
            .SetValue(reserva, DateTime.Now.AddDays(-4));

        // Act
        var estaValida = reserva.EstaValida();

        // Assert
        estaValida.Should().BeFalse();
    }
    #endregion
}