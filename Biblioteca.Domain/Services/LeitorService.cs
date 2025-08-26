using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Domain.Services;

public class LeitorService
{
    private readonly ILeitorRepository _leitorRepository;

    public LeitorService(ILeitorRepository leitorRepository)
    {
        _leitorRepository = leitorRepository;
    }
    public async Task<Leitor> ObterPorIdAsync(Guid id)
    {
        return await _leitorRepository.ObterPorIdAsync(id);
    }

    public async Task<IEnumerable<Leitor>> ObterTodosAsync()
    {
        return await _leitorRepository.ObterTodosAsync();
    }

    public async Task AdicionarLeitorAsync(Leitor leitor)
    {
        // Aqui você pode adicionar validações de negócio antes de salvar
        await _leitorRepository.SalvarAsync(leitor);
    }

    public async Task AtualizarLeitorAsync(Leitor leitor)
    {
        await _leitorRepository.AtualizarAsync(leitor);
    }

    public async Task RemoverLeitorAsync(Guid id)
    {
        await _leitorRepository.DeletarAsync(id);
    }
}