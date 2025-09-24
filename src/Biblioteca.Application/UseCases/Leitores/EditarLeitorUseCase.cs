using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.UseCases.Leitores;

public class EditarLeitorUseCase
{
    private readonly ILeitorRepository _leitorRepository;
    public EditarLeitorUseCase(ILeitorRepository leitorRepository)
    {
        _leitorRepository = leitorRepository;
    }

}