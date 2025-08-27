using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Application.UseCases.Leitores;

public class CadastrarLeitorUseCase
{
    private readonly ILeitorRepository _leitorRepo;

    public CadastrarLeitorUseCase(ILeitorRepository leitorRepo)
    {
        _leitorRepo = leitorRepo;
    }
    public async Task<ResultResponse<LeitorResponse>> Execute(CadastrarLeitorRequest request)
    {
        try
        {
            if (request == null)
                return ResultResponse<LeitorResponse>.Fail("Os dados do leitor são obrigatórios.");

            // Criando Value Objects
            var nome = new NomeCompleto(request.NomeCompleto.PrimeiroNome, request.NomeCompleto.UltimoNome);
            var email = new Email(request.Email.EnderecoEmail);
            var cpf = new CPF(request.Cpf.NumeroCpf);
            var endereco = new Endereco(request.Endereco.Rua, request.Endereco.Numero, request.Endereco.Bairro, request.Endereco.Cidade, request.Endereco.Estado, request.Endereco.Cep);

            // Criando entidade leitor
            var leitor = new Leitor(nome, email, cpf, endereco, request.DataCadastro);

            // Persistência
            await _leitorRepo.SalvarAsync(leitor);

            // Response
            var response = new LeitorResponse(
                leitor.Id,
                leitor.NomeCompleto.ToString(),
                leitor.Email.ToString(),
                leitor.CPF.ToString(),
                leitor.Endereco.ToString(),
                leitor.DataCadastro
            );

            return ResultResponse<LeitorResponse>.Ok(response, "Leitor cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultResponse<LeitorResponse>.Fail($"Erro ao cadastrar leitor: {ex.Message}");
        }
    }
}
