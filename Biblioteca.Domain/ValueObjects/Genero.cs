namespace Biblioteca.Domain.ValueObjects;

using Biblioteca.Domain.Shared.ValueObjects;

public class Genero : ValueObject
{
    public string Nome { get; }

    private static readonly List<string> GenerosValidos = new()
    {
        "Ficção",
        "Não Ficção",
        "Romance",
        "Fantasia",
        "Suspense",
        "Terror",
        "Biografia",
        "História",
        "Tecnologia"
    };

    public Genero(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O gênero não pode ser vazio.");

        if (!GenerosValidos.Contains(nome))
            throw new ArgumentException($"O gênero '{nome}' não é válido.");

        Nome = nome;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Nome;
    }

    public override string ToString()
    {
        return Nome;
    }
}