using Biblioteca.Application.DTOs.Requests;
using Biblioteca.Application.DTOs.Responses;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Biblioteca.Domain.Services;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Application.UseCases.Livros;

public class CadastrarLivroUseCase
{
    private readonly ILivroRepository _livroRepo;
    private readonly IAutorRepository _autorRepo;
    private readonly BibliotecarioService _bibliotecarioService;

    public CadastrarLivroUseCase(
        ILivroRepository livroRepo,
        IAutorRepository autorRepo,
        BibliotecarioService bibliotecarioService)
    {
        _livroRepo = livroRepo;
        _autorRepo = autorRepo;
        _bibliotecarioService = bibliotecarioService;
    }

    public async Task<ResultResponse<LivroResponse>> Execute(CadastrarLivroRequest request)
    {
        try
        {
            // 🔹 1. Validações de entrada
            if (string.IsNullOrWhiteSpace(request.Titulo))
                return ResultResponse<LivroResponse>.Fail("O título do livro é obrigatório.");

            if (request.NumeroPaginas <= 0)
                return ResultResponse<LivroResponse>.Fail("O número de páginas deve ser maior que zero.");

            if (request.Autor == null)
                return ResultResponse<LivroResponse>.Fail("As informações do autor são obrigatórias.");

            if (string.IsNullOrWhiteSpace(request.ISBN))
                return ResultResponse<LivroResponse>.Fail("O ISBN é obrigatório.");

            // 🔹 2. Verificar duplicidade de livro (ISBN único)
            var livroExistente = await _livroRepo.ObterPorISBNAsync(request.ISBN);
            if (livroExistente != null)
                return ResultResponse<LivroResponse>.Fail("Já existe um livro cadastrado com este ISBN.");

            // 🔹 3. Verificar se o autor já existe
            var autorExistente = await _autorRepo.ObterPorEmailAsync(request.Autor.Email);
            Autor autor = autorExistente ?? CriarNovoAutor(request);

            // 🔹 4. Salvar autor apenas se for novo
            if (autorExistente == null)
                await _autorRepo.SalvarAsync(autor);

            // 🔹 5. Mapear categorias
            var categorias = request.Categorias
                .Select(c => new Categoria(c.Nome, (Domain.Enums.ETipoCategoria)c.Tipo))
                .ToList();

            // 🔹 6. Criar livro via domínio
            var livro = _bibliotecarioService.CadastrarLivro(
                request.Titulo,
                autor,
                new ISBN(request.ISBN),
                request.AnoPublicacao,
                request.NumeroPaginas,
                categorias
            );

            await _livroRepo.SalvarAsync(livro);

            // 🔹 7. Montar resposta
            var response = new LivroResponse(
                livro.Id,
                livro.Titulo,
                livro.Autor.NomeCompleto.ToString(),
                livro.AnoPublicacao,
                livro.Disponivel
            );

            return ResultResponse<LivroResponse>.Ok(response, "Livro cadastrado com sucesso!");
        }
        catch (Exception ex)
        {
            return ResultResponse<LivroResponse>.Fail($"Erro interno ao cadastrar livro: {ex.Message}");
        }
    }

    // 🔹 Método auxiliar para criar novo autor
    private static Autor CriarNovoAutor(CadastrarLivroRequest request)
    {
        var nomes = request.Autor.NomeCompleto.Split(' ', 2);
        var primeiroNome = nomes[0];
        var sobrenome = nomes.Length > 1 ? nomes[1] : string.Empty;

        return new Autor(
            new NomeCompleto(primeiroNome, sobrenome),
            new Email(request.Autor.Email),
            request.Autor.DataNascimento
        );
    }
}
