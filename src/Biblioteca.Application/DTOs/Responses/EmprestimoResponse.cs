namespace Biblioteca.Application.DTOs.Responses;

//classe record pois imutável e possui um comportamento padrão para comparação.
//Esse use case representa a resposta de um empréstimo de livro.
public record EmprestimoResponse(Guid EmprestimoId, string NomeCompleto, string LivroTitulo, DateTime? DataEmprestimo, DateTime? DataPrevistaDevolucao, string Status);

