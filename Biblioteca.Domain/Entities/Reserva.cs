// Atributos: Usuário, Livro, Data da Reserva, Status (ativa, cancelada, atendida)
// Regras: Apenas possível se o livro estiver emprestado, reserva tem tempo de validade
// Métodos: Cancelar reserva, Tornar reserva ativa/inativa.
namespace Biblioteca.Domain.Entities;

public class Reserva
{
    #region Propriedades
    public Leitor Usuario { get; private set; }
    public Livro Livro { get; private set; }
    public DateTime DataReserva { get; private set; }
    public string Status { get; private set; } // ativa, cancelada, atendida
    #endregion
    
}