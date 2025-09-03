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

    public async Task<ResultResponse<LeitorResponse>> Execute(Guid id)
    {
        try
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(id);
            if (leitor == null)
                return ResultResponse<LeitorResponse>.Fail("Leitor não encontrado.");

            var response = new LeitorResponse(
                leitor.Id,
                leitor.NomeCompleto.ToString(),
                leitor.Email.ToString(),
                leitor.CPF.ToString(),
                leitor.Endereco.ToString(),
                leitor.DataCadastro
            );

            return ResultResponse<LeitorResponse>.Ok(response, "Leitor obtido com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultResponse<LeitorResponse>.Fail($"Erro ao obter leitor: {ex.Message}");
        }
    }
}