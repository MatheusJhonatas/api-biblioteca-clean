using System;
using System.Linq;
using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Services;

// ServicoDeEmprestimo.cs
public class ServicoDeEmprestimo
{
    public void RealizarEmprestimo(Leitor leitor, Emprestimo novoEmprestimo)
    {
        if (leitor.EstaInadimplente)
            throw new InvalidOperationException("Usuário está inadimplente e não pode realizar empréstimos.");

        int ativos = leitor.Emprestimos.Count(e => e.EmprestimoEmAndamento());

        if (ativos >= leitor.LimiteEmprestimosAtivos)
            throw new InvalidOperationException("Limite de empréstimos ativos atingido.");

        leitor.AdicionarEmprestimo(novoEmprestimo);
    }

    public void DevolverLivro(Leitor leitor, Guid emprestimoId)
    {
        var emprestimo = leitor.ObterEmprestimoPorId(emprestimoId);
        if (emprestimo == null)
            throw new InvalidOperationException("Empréstimo não encontrado.");

        emprestimo.FinalizarEmprestimo(DateTime.Now);
    }
}
