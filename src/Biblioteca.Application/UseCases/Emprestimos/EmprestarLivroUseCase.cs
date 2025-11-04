using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
namespace Biblioteca.Application.UseCases.Emprestimos;

public class EmprestarLivroUseCase
{
    private readonly ILeitorRepository _leitorRepo;
    private readonly ILivroRepository _livroRepo;
    private readonly IEmprestimoRepository _emprestimoRepo;
    private readonly IEmprestimoService _emprestimoService;

    public EmprestarLivroUseCase(ILeitorRepository leitorRepo, ILivroRepository livroRepo, IEmprestimoRepository emprestimoRepo, IEmprestimoService emprestimoService)
    {
        _leitorRepo = leitorRepo;
        _livroRepo = livroRepo;
        _emprestimoRepo = emprestimoRepo;
        _emprestimoService = emprestimoService;
    }

    public async Task<ResultResponse<EmprestimoResponse>> ExecuteAsync(EmprestarLivroRequest request)
    {
        try
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(request.LeitorId) ?? throw new ArgumentException("Leitor não encontrado.");
            var livro = await _livroRepo.ObterPorIdAsync(request.LivroId) ?? throw new ArgumentException("Livro não encontrado.");

            var emprestimo = _emprestimoService.RealizarEmprestimo(leitor, livro);
            await _emprestimoRepo.SalvarAsync(emprestimo);

            var response = new EmprestimoResponse(
                    emprestimo.Id,
                    emprestimo.Leitor.NomeCompleto.ToString(),
                    emprestimo.Livro.Titulo,
                    emprestimo.DataEmprestimo,
                    emprestimo.DataPrevistaDevolucao,
                    emprestimo.Status.ToString()
                );
            return ResultResponse<EmprestimoResponse>.Ok(response, "Empréstimo realizado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResultResponse<EmprestimoResponse>.Fail($"Erro ao realizar empréstimo: {ex.Message}");
        }
    }
}
