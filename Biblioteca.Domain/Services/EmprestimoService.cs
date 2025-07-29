using Biblioteca.Domain.Entities;
using System;
using System.Linq;

namespace Biblioteca.Domain.Services
{
    public class EmprestimoService
    {
        public Emprestimo RealizarEmprestimo(Leitor leitor, Livro livro)
        {
            if (leitor == null) throw new ArgumentNullException(nameof(leitor));
            if (livro == null) throw new ArgumentNullException(nameof(livro));

            if (leitor.EstaInadimplente)
                throw new InvalidOperationException("Leitor inadimplente não pode realizar empréstimos.");

            int emprestimosAtivos = leitor.Emprestimos.Count(e => e.EmprestimoEmAndamento());
            if (emprestimosAtivos >= leitor.LimiteEmprestimosAtivos)
                throw new InvalidOperationException("Limite de empréstimos ativos atingido.");

            if (!livro.Disponivel)
                throw new InvalidOperationException("Livro indisponível para empréstimo.");

            var dataEmprestimo = DateTime.Now;
            var dataPrevista = dataEmprestimo.AddDays(7); // Regra: 7 dias para devolução

            var emprestimo = new Emprestimo(leitor, livro, dataEmprestimo, dataPrevista);
            leitor.AdicionarEmprestimo(emprestimo);

            livro.Emprestar(); // Marca como indisponível

            return emprestimo;
        }

        public void DevolverLivro(Leitor leitor, Guid emprestimoId)
        {
            if (leitor == null) throw new ArgumentNullException(nameof(leitor));

            var emprestimo = leitor.ObterEmprestimoPorId(emprestimoId);
            if (emprestimo == null)
                throw new InvalidOperationException("Empréstimo não encontrado.");

            emprestimo.FinalizarEmprestimo(DateTime.Now);
        }
    }
}
