// Atributos: Nome completo (VO), Email (VO), CPF (VO), Endereço (VO), Data de Cadastro, Lista de Empréstimos
// Regras: Pode emprestar livros, possui um limite de empréstimos ativos, pode estar inadimplente (ex: se há empréstimos atrasados)
// Métodos: Realizar empréstimo, Devolver livro, Verificar inadimplência, Atualizar endereço
using System;
using System.Collections.Generic;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Leitor
{
    #region Propriedades
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public CPF CPF { get; private set; }
    public Endereco Endereco { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public List<Emprestimo> Emprestimos { get; private set; }
    #endregion
    #region Construtores
    public Leitor(NomeCompleto nomeCompleto, Email email, CPF cPF, Endereco endereco, DateTime dataCadastro, List<Emprestimo> emprestimos)
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        CPF = cPF;
        Endereco = endereco;
        DataCadastro = dataCadastro;
        Emprestimos = emprestimos;
    }
    #endregion
    #region Metodos
    //Realizar empréstimo,
    public void Emprestimo()
    {

    }
    //  Devolver livro, 
    public void DevolverLivro()
    {

    }
    // Verificar inadimplência
    public void VerificarInadiplencia()
    {

    }
    //  Atualizar endereço
    public void AtualizarEndereco()
    {

    }
    #endregion
}