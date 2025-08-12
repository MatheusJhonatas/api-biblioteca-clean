// Atributos: Nome completo(VO), Email(VO), Data de Nascimento
// Regras: Pode ter múltiplos livros publicados
// Métodos: Nenhum comportamento complexo previsto, mais leitura do que escrita
using System;
using Biblioteca.Domain.ValueObjects;
namespace Biblioteca.Domain.Entities;

public class Autor : Entity
{
    #region Propriedades
    // Propriedades do Autor, incluindo NomeCompleto e Email como Value Objects
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public DateTime DataNascimento { get; private set; }
    #endregion
    #region Construtores
    // Construtor da classe Autor que recebe NomeCompleto, Email e DataNascimento.
    public Autor() : base(Guid.NewGuid()) { }
    public Autor(NomeCompleto nomeCompleto, Email email, DateTime dataNascimento) : base(Guid.NewGuid())
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        DataNascimento = dataNascimento;
    }
    #endregion
}