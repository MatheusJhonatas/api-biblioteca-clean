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

    public async Task<ResultResponse<LeitorResponse>> ExecuteAsync(Guid leitorId, EditarLeitorRequest request)
    {
        var leitor = await _leitorRepository.ObterPorIdAsync(leitorId);
        if (leitor == null)
            throw new ArgumentException("Leitor não encontrado.");

        if (!string.IsNullOrWhiteSpace(request.NovoPrimeiroNome) || !string.IsNullOrWhiteSpace(request.NovoSegundoNome))
            leitor.AlterarNomeCompleto(new NomeCompleto(request.NovoPrimeiroNome ?? "", request.NovoSegundoNome ?? ""));

        if (!string.IsNullOrWhiteSpace(request.NovoEmail))
            leitor.AlterarEmail(new Email(request.NovoEmail));

        if (!string.IsNullOrWhiteSpace(request.NovoCPF))
            leitor.AlterarCPF(new CPF(request.NovoCPF));

        if (request.NovoEndereco != null)
            leitor.AtualizarEndereco(new Endereco(
                request.NovoEndereco.Rua,

                request.NovoEndereco.Numero,
                request.NovoEndereco.Bairro,
                request.NovoEndereco.Cidade,
                request.NovoEndereco.Estado,
                request.NovoEndereco.Cep,
                request.NovoEndereco.Complemento
            ));
        await _leitorRepository.AtualizarAsync(leitor);
        // Retorna o leitor atualizado
        return ResultResponse<LeitorResponse>.Ok(new LeitorResponse(
            leitor.Id,
            leitor.NomeCompleto.ToString(),
            leitor.Email.ToString(),
            leitor.CPF.ToString(),
            leitor.Endereco.ToString(),
            leitor.DataCadastro
        ), $"Leitor {leitor.NomeCompleto} atualizado com sucesso!");
    }
}
