// Usuário/Leitor (User ou Reader)
// Atributos: Nome, Email, CPF, Endereço, Data de Cadastro.

// Regras: Pode emprestar livros, tem limite de empréstimos, pode estar inadimplente.
public sealed class Usuario
{

    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public Usuario(string nome, string email, string senha)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
    }
}