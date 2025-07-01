using Biblioteca.Domain.Shared.Entities;
namespace Biblioteca.Domain.Entities;

public class Usuario : Entity
{
    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public string Nome { get; private set; } 
    //private set; é usado para que o valor não possa ser alterado após a criação do objeto
    public string Email { get; private set; }
    
}