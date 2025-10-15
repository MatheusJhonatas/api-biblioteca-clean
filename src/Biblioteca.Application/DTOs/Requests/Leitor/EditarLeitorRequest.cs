namespace Biblioteca.Application.DTOs.Requests.Leitor;

public record EditarLeitorRequest(string? NovoPrimeiroNome, string? NovoSegundoNome, string? NovoEmail, string? NovoCPF, EnderecoRequest? NovoEndereco);