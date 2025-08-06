using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface IReservaRepository
{
    Reserva ObterPorId(Guid id);
    void Salvar(Reserva reserva);
    void Atualizar(Reserva reserva);
    IEnumerable<Reserva> ObterPorLivro(Guid livroId);
}