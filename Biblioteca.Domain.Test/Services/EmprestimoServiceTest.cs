using Xunit;
using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.ValueObjects;

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
    #endregion
}