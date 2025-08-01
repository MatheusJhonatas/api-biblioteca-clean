using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.Entites;

public class LeitorTest
{
    #region Métodos de Fábrica
    /// <summary>
    /// Cria um leitor válido para testes.
    /// </summary>
    private Leitor CriarLeitorValido()
    {
        var nome = new NomeCompleto("Ana", "Beatriz");
        var email = new Email("anabbrandao155@gmail.com");
        var cpf = new CPF("52998224725");
        var endereco = new Endereco(
            rua: "Rua Antonio Santana Leite",
            numero: "540",
            bairro: "Centro",
            cidade: "São Paulo",
            estado: "SP",
            cep: "06515004"
        );
        var dataCadastro = DateTime.Today;

        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }
    #endregion

    #region Testes Unitários
    [Fact(DisplayName = "Dado um leitor válido, deve possuir todos os dados corretos")]
    public void Dado_Leitor_Valido_Deve_Possuir_Dados_Corretos()
    {
        // Arrange
        var leitor = CriarLeitorValido();

        // Assert
        leitor.NomeCompleto.PrimeiroNome.Should().Be("Ana");
        leitor.NomeCompleto.UltimoNome.Should().Be("Beatriz");
        leitor.Email.EnderecoEmail.Should().Be("anabbrandao155@gmail.com");
        leitor.CPF.Numero.Should().Be("52998224725");
        leitor.Endereco.Rua.Should().Be("Rua Antonio Santana Leite");
        leitor.Endereco.Numero.Should().Be("540");
        leitor.Endereco.Bairro.Should().Be("Centro");
        leitor.Endereco.Cidade.Should().Be("São Paulo");
        leitor.Endereco.Estado.Should().Be("SP");
        leitor.Endereco.CEP.Should().Be("06515004");
        leitor.DataCadastro.Should().Be(DateTime.Today);
    }
    #endregion
}
