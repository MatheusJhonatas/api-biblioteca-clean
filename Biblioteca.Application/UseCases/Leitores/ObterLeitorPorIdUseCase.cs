using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Leitores;

public class ObterLeitorPorIdUseCase
{
    private readonly ILeitorRepository _leitorRepo;

    public ObterLeitorPorIdUseCase(ILeitorRepository leitorRepo)
    {
        _leitorRepo = leitorRepo;
    }

    public async Task<ResultResponse<LeitorResponse>> ExecuteAsync(Guid id)
    {
        try
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(id);

            if (leitor == null)
                return ResultResponse<LeitorResponse>.Fail("Leitor não encontrado.");

            // DEBUG: logar antes de acessar propriedades
            Console.WriteLine($"[DEBUG] Leitor encontrado: {leitor.Id}");
            Console.WriteLine($"[DEBUG] NomeCompleto: {(leitor.NomeCompleto == null ? "NULL" : leitor.NomeCompleto.ToString())}");
            Console.WriteLine($"[DEBUG] Email: {(leitor.Email == null ? "NULL" : leitor.Email.ToString())}");
            Console.WriteLine($"[DEBUG] CPF: {(leitor.CPF == null ? "NULL" : leitor.CPF.ToString())}");
            Console.WriteLine($"[DEBUG] Endereco: {(leitor.Endereco == null ? "NULL" : leitor.Endereco.ToString())}");

            var response = new LeitorResponse(
                leitor.Id,
                leitor.NomeCompleto?.ToString() ?? string.Empty,
                leitor.Email?.ToString() ?? string.Empty,
                leitor.CPF?.ToString() ?? string.Empty,
                leitor.Endereco?.ToString() ?? string.Empty,
                leitor.DataCadastro
            );

            return ResultResponse<LeitorResponse>.Ok(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine("[DEBUG] StackTrace: " + ex.StackTrace);
            return ResultResponse<LeitorResponse>.Fail($"Erro ao obter leitor: {ex.Message}");
        }
    }
}