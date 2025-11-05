using Biblioteca.Application.DTOs.Requests.Emprestimos;
using Biblioteca.Application.DTOs.Requests.Livro;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
namespace Biblioteca.Application.UseCases.Emprestimos;

public class DevolverLivroUseCase
{
    private readonly ILeitorRepository _leitorRepo;
    private readonly IEmprestimoRepository _emprestimoRepo;
    private readonly IEmprestimoService _emprestimoService;

    public DevolverLivroUseCase(
        ILeitorRepository leitorRepo,
        IEmprestimoRepository emprestimoRepo,
        IEmprestimoService emprestimoService)
    {
        _leitorRepo = leitorRepo;
        _emprestimoRepo = emprestimoRepo;
        _emprestimoService = emprestimoService;
    }

    public async Task<ResultResponse<EmprestimoResponse>> ExecuteAsync(DevolverLivroRequest request)
    {
        try
        {
            var emprestimo = await _emprestimoRepo.ObterPorIdAsync(request.EmprestimoId)
                ?? throw new ArgumentException("Empréstimo não encontrado.");
            //devolver o livro
            _emprestimoService.DevolverLivro(emprestimo);
            //atualizar o empréstimo no repositório
            await _emprestimoRepo.AtualizarAsync(emprestimo);
            //retornar sucesso
            var response = new EmprestimoResponse(
                emprestimo.Id,
                emprestimo.Leitor.NomeCompleto.PrimeiroNome,
                emprestimo.Livro.Titulo,
                emprestimo.DataEmprestimo,
                emprestimo.DataPrevistaDevolucao,
                emprestimo.Status.ToString()
            );
            return ResultResponse<EmprestimoResponse>.Ok(response, "Livro devolvido com sucesso.");
        }
        catch (Exception ex)
        {
            return ResultResponse<EmprestimoResponse>.Fail("Erro ao devolver livro: " + ex.Message);
        }
    }
}

