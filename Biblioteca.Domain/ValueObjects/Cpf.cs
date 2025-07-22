// Atributos: Número do CPF
// Regras: Deve ser um CPF válido (com validação de dígitos verificadores)
// Uso: User
namespace Biblioteca.Domain.ValueObjects;

public class CPF : ValueObject
{
    #region Propriedades
    public string Numero { get; private set; }
    #endregion
    #region Construtores
    // Construtor da classe CPF que recebe o número do CPF e verifica se é válido

    public CPF(string numero)
    {
        if (string.IsNullOrWhiteSpace(numero) || !IsValid(numero))
            throw new ArgumentException("CPF inválido.", nameof(numero));

        Numero = numero;
    }
    #endregion
    #region Métodos
    private bool IsValid(string cpf)
    {
        // Implementar a lógica de validação do CPF
        // Exemplo: Verificar se o CPF tem 11 dígitos e calcular os dígitos verificadores
        return true; // Placeholder para a lógica de validação real
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Numero;
    }
    #endregion
}
