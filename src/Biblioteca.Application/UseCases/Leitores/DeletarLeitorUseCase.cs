using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Leitores;

public class DeletarLeitorUseCase
{
    private readonly ILeitorRepository _leitorRepository;

    public DeletarLeitorUseCase(ILeitorRepository leitorRepository)
    {
        _leitorRepository = leitorRepository;
    }

    public async Task<ResultResponse<string>> ExecuteAsync(Guid id)
    {
        try
        {
            var leitor = await _leitorRepository.ObterPorIdAsync(id);
            if (leitor == null)
                return ResultResponse<string>.Fail("Leitor não encontrado.");

            await _leitorRepository.DeletarAsync(id);

            return ResultResponse<string>.Ok($"Leitor {leitor.NomeCompleto.PrimeiroNome} deletado com sucesso.");
        }
        catch (Exception ex)
        {
            return ResultResponse<string>.Fail($"Erro ao deletar leitor: {ex.Message}");
        }
    }
}
