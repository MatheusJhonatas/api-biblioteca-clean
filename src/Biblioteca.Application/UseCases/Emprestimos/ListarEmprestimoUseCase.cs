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

    public async Task<ResultResponse<List<EmprestimoResponse>>> ExecuteAsync()
    {
        try
        {
            var emprestimos = await _emprestimoRepo.ListarTodosAsync();
            var response = emprestimos.Select(e => new EmprestimoResponse(
                e.Id,
                e.Leitor.NomeCompleto.ToString(),
                e.Livro.Titulo,
                e.DataEmprestimo,
                e.DataPrevistaDevolucao,
                e.Status.ToString()
            )).ToList();

            return ResultResponse<List<EmprestimoResponse>>.Ok(response);
        }
        catch (Exception ex)
        {
            return ResultResponse<List<EmprestimoResponse>>.Fail($"Erro ao listar empréstimos: {ex.Message}");
        }
    }
}