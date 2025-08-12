// Atributos: Usuário, Livro, Data da Reserva, Status (ativa, cancelada, atendida)
// Regras: Apenas possível se o livro estiver emprestado, reserva tem tempo de validade
// Métodos: Cancelar reserva, Tornar reserva ativa/inativa.

using System;
using Biblioteca.Domain.Enums;
using Biblioteca.Domain.Extensions;

namespace Biblioteca.Domain.Entities;

public class Reserva : Entity
{
    #region Propriedades
    public Leitor Usuario { get; private set; }
    public Livro Livro { get; private set; }
    public Guid LivroId { get; private set; }
    public DateTime DataReserva { get; private set; }
    public EStatusReserva Status { get; private set; } // ativa, cancelada, atendida
    #endregion
    #region Construtor
    public Reserva() : base(Guid.NewGuid()) { }
    public Reserva(Leitor usuario, Livro livro)
        : base(Guid.NewGuid())
    {
        Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        Livro = livro ?? throw new ArgumentNullException(nameof(livro));
        DataReserva = DateTime.Now.ToBrasiliaTime();
        Status = EStatusReserva.Ativa;
    }
    #endregion
    #region Metodos
    public void Cancelar()
    {
        if (Status != EStatusReserva.Ativa)
            throw new InvalidOperationException("Reserva já cancelada ou atendida.");
        Status = EStatusReserva.Cancelada;
    }

    public void MarcarComoAtendida()
    {
        if (Status != EStatusReserva.Ativa)
            throw new InvalidOperationException("A reserva já foi finalizada.");
        Status = EStatusReserva.Atendida;
    }

    public bool EstaValida()
    {
        return Status == EStatusReserva.Ativa && DataReserva.AddDays(3) > DateTime.Now.ToBrasiliaTime();
    }
    #endregion
}