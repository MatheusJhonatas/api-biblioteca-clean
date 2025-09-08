using System;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Enums;

namespace Biblioteca.Domain.Services
{
    public class ReservaService
    {
        // Cria uma nova reserva se o livro estiver emprestado.
        public Reserva CriarReserva(Leitor leitor, Livro livro)
        {
            if (leitor == null) throw new ArgumentNullException(nameof(leitor));
            if (livro == null) throw new ArgumentNullException(nameof(livro));

            if (livro.Disponivel)
                throw new InvalidOperationException("Não é possível reservar um livro disponível.");

            return new Reserva(leitor, livro);
        }
        // Cancela uma reserva ativa.
        public void CancelarReserva(Reserva reserva)
        {
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));

            reserva.Cancelar();
        }
        // Marca a reserva como atendida.
        public void AtenderReserva(Reserva reserva)
        {
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));

            reserva.MarcarComoAtendida();
        }
        // Verifica se a reserva ainda é válida (prazo de 3 dias).
        public bool ReservaEstaValida(Reserva reserva)
        {
            if (reserva == null) throw new ArgumentNullException(nameof(reserva));

            return reserva.Status == EStatusReserva.Ativa &&
                   reserva.DataReserva.AddDays(3) > DateTime.Now;
        }
    }
}
