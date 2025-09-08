using Biblioteca.Domain.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Biblioteca.Domain.Test.ValueObjects;

public class CpfTest
{
    #region Testes Unitários
    [Fact]
    public void Nao_Deve_Criar_Cpf_Com_Numero_Nulo()
    {
        Action action = () => new CPF(null);
        action.Should().Throw<ArgumentException>()
            .WithMessage("CPF não pode ser nulo.*")
            .And.ParamName.Should().Be("numero");
    }

    [Fact]
    public void Nao_Deve_Criar_Cpf_Com_Numero_Vazio()
    {
        Action action = () => new CPF("");
        action.Should().Throw<ArgumentException>()
            .WithMessage("CPF não pode ser vazio.*")
            .And.ParamName.Should().Be("numero");
    }

    #endregion
}