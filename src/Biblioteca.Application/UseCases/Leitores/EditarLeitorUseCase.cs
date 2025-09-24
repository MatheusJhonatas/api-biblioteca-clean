using Biblioteca.Application.DTOs.Requests.Leitor;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Application.UseCases.Leitores;

public class EditarLeitorUseCase
{
    private readonly ILeitorRepository _leitorRepository;

    public EditarLeitorUseCase(ILeitorRepository leitorRepository)
    {
        _leitorRepository = leitorRepository;
    }

    public async Task Execute(EditarLeitorDto dto)
    {
        var leitor = await _leitorRepository.ObterPorIdAsync(dto.Id);
        if (leitor == null)
            throw new InvalidOperationException("Leitor não encontrado.");

        // delega ao domínio
        leitor.AtualizarDados(
            new NomeCompleto(dto.PrimeiroNome, dto.Sobrenome),
            new Email(dto.Email),
            new CPF(dto.CPF),
            new Endereco()
        );

        await _leitorRepository.AtualizarAsync(leitor);
    }
}
