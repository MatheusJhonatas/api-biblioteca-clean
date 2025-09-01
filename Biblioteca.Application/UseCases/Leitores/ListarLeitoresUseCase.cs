using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Leitores;

public class ListarLeitoresUseCase
{
    private readonly ILeitorRepository _leitorRepo;

    public ListarLeitoresUseCase(ILeitorRepository leitorRepo)
    {
        _leitorRepo = leitorRepo;
    }

    public async Task<ResultResponse<List<LeitorResponse>>> Execute()
    {
        try
        {
            var leitores = await _leitorRepo.ObterTodosAsync();

            var response = leitores.Select(leitor => new LeitorResponse(
                leitor.Id,
                leitor.NomeCompleto.ToString(),
                leitor.Email.ToString(),
                leitor.CPF.ToString(),
                leitor.Endereco.ToString(),
                leitor.DataCadastro
            )).ToList();

            return ResultResponse<List<LeitorResponse>>.Ok(response, "Leitores listados com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultResponse<List<LeitorResponse>>.Fail($"Erro ao listar leitores: {ex.Message}");
        }
    }
}