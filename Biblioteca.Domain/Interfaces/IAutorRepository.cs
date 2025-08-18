using Biblioteca.Domain.Entities;

namespace Biblioteca.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<Autor?> ObterPorEmailAsync(string email);
        Task<Autor?> ObterPorIdAsync(Guid id);
        void Salvar(Autor autor);
    }
}
