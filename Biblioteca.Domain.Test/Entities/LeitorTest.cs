using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test;

public class LeitorTest
{
    #region FactoryMethods
    private Leitor Criar_Leitor_Valido()
    {
        var nome = new NomeCompleto("Ana", "Beatriz");
        var email = new Email("anabbrandao155@gmail.com");
        var cpf = new CPF("44879352888");
        var endereco = new Endereco("Rua Antonio Santana Leite", "540", "SãoLuiz", "Santana de Parnaiba", "SP", "06515-005");
        var dataCadastro = DateTime.Now;
        return new Leitor(nome, email, cpf, endereco, dataCadastro);
    }
    #endregion
    #region Testes Unitário
    [Fact]
    public void Dado_Leitor_Valido_Deve_Possuir_Dados_Corretos()
    {
        //Arrange
        var leitor = Criar_Leitor_Valido();
        //Assert
        leitor.NomeCompleto.PrimeiroNome.Should().Be("Ana");
        leitor.Email.EnderecoEmail.Should().Be("anabbrandao155@gmail.com");
        // leitor.Endereco.Cidade.Should().Be("Santana de Parnaiba");
        leitor.DataCadastro.Date.Should().Be(DateTime.Now.Date);
    }
    #endregion 
}