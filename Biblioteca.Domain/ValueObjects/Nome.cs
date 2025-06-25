using Biblioteca.Domain.Shared.ValueObjects;

namespace Biblioteca.Domain.ValueObjects;

public sealed record Nome : ValueObject
{
    #region Constructors
    public Nome(string primeiroNome, string ultimoNome)
    {
        PrimeiroNome = primeiroNome;
        UltimoNome = ultimoNome;
    }
    #endregion
    
    #region Properties
    public string PrimeiroNome { get; }
    public string UltimoNome { get; }
    #endregion
    
    #region Operators
    public static implicit operator string(Nome nome) => nome.ToString();
    #endregion
    
    #region Overrides
    public override string ToString() => $"{PrimeiroNome} {UltimoNome}";
    #endregion
    
}