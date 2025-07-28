// Atributos: Nome completo (VO), Email (VO), CPF (VO), Endereço (VO), Data de Cadastro, Lista de Empréstimos
// Regras: Pode emprestar livros, possui um limite de empréstimos ativos, pode estar inadimplente (ex: se há empréstimos atrasados)
// Métodos: Realizar empréstimo, Devolver livro, Verificar inadimplência, Atualizar endereço
using System;
using System.Collections.Generic;
using System.Linq;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Leitor
{
    public string Id { get; private set; }
    private IList<Emprestimo> _emprestimos;
    #region Propriedades
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public CPF CPF { get; private set; }
    public Endereco Endereco { get; private set; }
    public DateTime DataCadastro { get; private set; }
    public IReadOnlyCollection<Emprestimo> Emprestimos { get { return _emprestimos.ToArray(); } }
    public bool EstaInadimplente => _emprestimos.Any(e => e.EstaAtrasado());
    public int LimiteEmprestimosAtivos { get; private set; } = 5;
    #endregion
    #region Construtores
    public Leitor(NomeCompleto nomeCompleto, Email email, CPF cPF, Endereco endereco, DateTime dataCadastro)
    {
        Id = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        NomeCompleto = nomeCompleto;
        Email = email;
        CPF = cPF;
        Endereco = endereco;
        DataCadastro = dataCadastro;
        _emprestimos = new List<Emprestimo>();
    }
    #endregion
    #region Metodos
    // Verificar inadimplência
    public bool VerificarInadiplencia()
    {
        return EstaInadimplente;
    }
    //  Atualizar endereço
    public void AtualizarEndereco(Endereco novoEndereco)
    {
        if (novoEndereco == null)
            throw new ArgumentNullException(nameof(novoEndereco));

        Endereco = novoEndereco;
    }
    internal void AdicionarEmprestimo(Emprestimo emprestimo)
    {
        _emprestimos.Add(emprestimo);
    }
    internal Emprestimo ObterEmprestimoPorId(Guid id)
    {
        return _emprestimos.FirstOrDefault(e => e.Id == id);
    }
    #endregion
}