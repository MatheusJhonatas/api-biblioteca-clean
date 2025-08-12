using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;
//Interface de Repositório para Reservas, essa interface é utilizada para gerenciar as operações relacionadas às reservas no banco de dados.    
public interface IReservaRepository
{
    Reserva ObterPorId(Guid id);
    void Salvar(Reserva reserva);
    void Atualizar(Reserva reserva);
    IEnumerable<Reserva> ObterPorLivro(Guid livroId);
}