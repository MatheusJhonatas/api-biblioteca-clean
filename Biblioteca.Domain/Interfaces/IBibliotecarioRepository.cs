using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces;

public interface IBibliotecarioRepository
{
    Bibliotecario ObterPorId(Guid id);
    void Salvar(Bibliotecario bibliotecario);
}