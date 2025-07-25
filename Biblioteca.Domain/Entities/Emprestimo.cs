// Atributos: Usuário, Livro, Data de Empréstimo, Data Prevista de Devolução, Data Real de Devolução, Status
// Regras: Um empréstimo só pode ser criado se o usuário não estiver inadimplente e o livro estiver disponível
// Métodos: Finalizar empréstimo (com devolução), Calcular multa por atraso

using System;
using System.Collections.Generic;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Entities;

public class Emprestimo
{
    #region Propriedades
    // Propriedades do Empréstimo, incluindo referências ao Usuário e Livro.
    public Leitor Usuario { get; private set; }
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Livro Livro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public DateTime? DataRealDevolucao { get; private set; }
    public EStatusEmprestimo Status { get; private set; }
    #endregion
    #region Construtores
    // Construtor para inicializar um novo empréstimo.
    public Emprestimo(Leitor usuario, Livro livro, DateTime dataEmprestimo, DateTime dataPrevistaDevolucao)
    {
        Usuario = usuario;
        Livro = livro;
        DataEmprestimo = dataEmprestimo;
        DataPrevistaDevolucao = dataPrevistaDevolucao;
        Status = EStatusEmprestimo.Ativo; // Inicialmente o status é "Ativo".
    }
    public Emprestimo() // Construtor vazio para ORM ou serialização.
    {
        Emprestos = new List<Emprestimo>();
    }

    #endregion
    #region Metodos
    // Métodos para gerenciar o empréstimo, como finalizar, calcular multas, etc.
    public void FinalizarEmprestimo(DateTime dataDevolucao)
    {
        DataRealDevolucao = dataDevolucao;
        Status = EStatusEmprestimo.Finalizado;
        // Lógica para calcular multas, se necessário.
    }
    public decimal CalcularMulta()
    {
        if (DataRealDevolucao.HasValue && DataRealDevolucao > DataPrevistaDevolucao)
        {
            var diasAtraso = (DataRealDevolucao.Value - DataPrevistaDevolucao).Days;
            // Supondo uma multa fixa por dia de atraso, por exemplo, R$ 1,00 por dia.
            return diasAtraso * 1.00m;
        }
        return 0.00m; // Sem multa se não houver atraso.
    }
    public bool EstaAtivo()
    {
        return Status == EStatusEmprestimo.Ativo;
    }
    public bool EstaAtrasado()
    {
        return DataRealDevolucao.HasValue && DataRealDevolucao > DataPrevistaDevolucao;
    }
    public void AtualizarStatus(EStatusEmprestimo novoStatus)
    {
        Status = novoStatus;
    }
    public List<Emprestimo> Emprestos { get; private set; } // Lista de empréstimos associados ao usuário.
    public void AdicionarEmpresto(Emprestimo emprestimo)
    {
        if (Emprestos == null)
        {
            Emprestos = new List<Emprestimo>();
        }
        Emprestos.Add(emprestimo);
    }
    public bool EmprestimoEmAndamento()
    {
        return Status == EStatusEmprestimo.Ativo && DataRealDevolucao == null;
    }
    #endregion
}