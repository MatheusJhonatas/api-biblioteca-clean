using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Extensions;

public class Emprestimo : Entity
{
    #region Propriedades
    public Leitor Usuario { get; private set; }
    public Guid UsuarioId => Usuario.Id;
    public Guid LivroId => Livro.Id;

    public Livro Livro { get; private set; }
    public DateTime DataEmprestimo { get; private set; }
    public DateTime DataPrevistaDevolucao { get; private set; }
    public DateTime? DataRealDevolucao { get; private set; }
    public EStatusEmprestimo Status { get; private set; }
    #endregion
    #region Construtor
    public Emprestimo() : base(Guid.NewGuid()) { }

    public Emprestimo(Leitor usuario, Livro livro, DateTime dataEmprestimo, DateTime dataPrevistaDevolucao)
        : base(Guid.NewGuid())
    {
        Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        Livro = livro ?? throw new ArgumentNullException(nameof(livro));
        DataEmprestimo = dataEmprestimo;
        DataPrevistaDevolucao = dataPrevistaDevolucao;
        Status = EStatusEmprestimo.Ativo;
    }
    #endregion
    #region Metodos
    public void FinalizarEmprestimo(DateTime dataDevolucao)
    {
        if (Status != EStatusEmprestimo.Ativo)
            throw new InvalidOperationException("Este empréstimo já foi finalizado.");

        DataRealDevolucao = dataDevolucao;
        Status = EStatusEmprestimo.Finalizado;
        if (!Livro.Disponivel) // <- adiciona essa proteção aqui
            Livro.Devolver();
    }

    public bool EstaAtrasado()
    {
        var referencia = DataRealDevolucao ?? DateTime.Now.ToBrasiliaTime();
        return referencia > DataPrevistaDevolucao;
    }

    public decimal CalcularMulta()
    {
        if (!EstaAtrasado()) return 0m;

        var diasAtraso = ((DataRealDevolucao ?? DateTime.Now.ToBrasiliaTime()) - DataPrevistaDevolucao).Days;
        return diasAtraso * 1.00m;
    }

    public bool EmprestimoEmAndamento() =>
        Status == EStatusEmprestimo.Ativo && !DataRealDevolucao.HasValue;
    #endregion
}
