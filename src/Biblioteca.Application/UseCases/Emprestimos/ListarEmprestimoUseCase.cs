using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Emprestimos;

public class ListarEmprestimoUseCase
{
    private readonly IEmprestimoRepository _emprestimoRepo;

    public ListarEmprestimoUseCase(IEmprestimoRepository emprestimoRepo)
    {
        _emprestimoRepo = emprestimoRepo;
    }

    public async Task<IEnumerable<EmprestimoResponse>> ExecuteAsync()
    {
        var emprestimos = await _emprestimoRepo.ListarTodosAsync();
        return emprestimos.Select(e => new EmprestimoResponse(e.Id, e.DataEmprestimo, e.DataPrevistaDevolucao));
    }
}