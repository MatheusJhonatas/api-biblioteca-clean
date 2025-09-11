using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;

public class DeletarLeitorUseCase
{
    // Aqui é onde você consome o repositório para deletar um leitor, isso é injetado via construtor
    private readonly ILeitorRepository _leitorRepo;
    public DeletarLeitorUseCase(ILeitorRepository leitorRepo)
    {
        _leitorRepo = leitorRepo;
    }
    public async Task<ResultResponse<LeitorResponse>> DeletarAsync(Guid id)
    {
        try
        {
            var leitor = await _leitorRepo.ObterPorIdAsync(id);
            if (leitor == null)
                return ResultResponse<LeitorResponse>.Fail("Leitor não encontrado.");
            await _leitorRepo.DeletarAsync(id);
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
            return ResultResponse<LeitorResponse>.Fail($"Erro ao deletar leitor: {ex.Message}");
        }
    }
}