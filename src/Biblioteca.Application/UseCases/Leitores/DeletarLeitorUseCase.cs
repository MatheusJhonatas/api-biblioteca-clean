using Biblioteca.Domain.Interfaces;

public class DeletarLeitorUseCase
{
    // Aqui é onde você consome o repositório para deletar um leitor, isso é injetado via construtor
    private readonly ILeitorRepository _leitorRepo;
    public DeletarLeitorUseCase(ILeitorRepository leitorRepo)
    {
        _leitorRepo = leitorRepo;
    }
}