namespace Biblioteca.Domain.ValueObjects;

public class Email
{
    public string Endereco { get; private set; }

    public Email(string endereco)
    {
        if (string.IsNullOrWhiteSpace(endereco))
            throw new ArgumentException("O endereço de email não pode ser vazio.", nameof(endereco));

        if (!IsValidEmail(endereco))
            throw new ArgumentException("Endereço de email inválido.", nameof(endereco));

        Endereco = endereco;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}