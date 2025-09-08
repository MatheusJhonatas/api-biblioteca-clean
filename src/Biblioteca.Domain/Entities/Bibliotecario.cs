// Atributos: Nome completo (VO), Email (VO), Matrícula, Cargo
// Regras: Pode cadastrar livros, excluir, atualizar cadastros, processar devoluções
// Métodos: Cadastrar livro, Atualizar estoque, Visualizar empréstimos ativos
using System;
using Biblioteca.Domain.ValueObjects;

namespace Biblioteca.Domain.Entities;

public class Bibliotecario : Entity
{
    #region Propriedades
    public NomeCompleto NomeCompleto { get; private set; }
    public Email Email { get; private set; }
    public string Matricula { get; private set; }
    public string Cargo { get; private set; }
    #endregion

    #region Construtores
    // Construtor para inicializar as propriedades do bibliotecário.
    public Bibliotecario() : base(Guid.NewGuid()) { }

    public Bibliotecario(NomeCompleto nomeCompleto, Email email, string matricula, string cargo)
         : base(Guid.NewGuid())
    {
        NomeCompleto = nomeCompleto ?? throw new ArgumentNullException(nameof(nomeCompleto));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(matricula))
            throw new ArgumentException("Matrícula inválida.", nameof(matricula));

        if (string.IsNullOrWhiteSpace(cargo))
            throw new ArgumentException("Cargo inválido.", nameof(cargo));
        Matricula = matricula;
        Cargo = cargo;

    }
    #endregion
}