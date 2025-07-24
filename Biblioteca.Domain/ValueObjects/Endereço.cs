// Atributos: Rua, Número, Bairro, Cidade, Estado, CEP
// Regras: Todos os campos obrigatórios; CEP deve ser válido
// Uso: User, possivelmente Library
using System;
using System.Collections.Generic;
namespace Biblioteca.Domain.ValueObjects;

public record class Endereco : ValueObject
{
    #region Propriedades
    public string Rua { get; private set; }
    public string Numero { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string CEP { get; private set; }
    #endregion

    #region Construtores
    public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(rua) || string.IsNullOrWhiteSpace(numero) ||
            string.IsNullOrWhiteSpace(bairro) || string.IsNullOrWhiteSpace(cidade) ||
            string.IsNullOrWhiteSpace(estado) || !IsValidCEP(cep))
        {
            throw new ArgumentException("Endereço inválido.");
        }

        Rua = rua;
        Numero = numero;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        CEP = cep;
    }
    #endregion

    #region Métodos
    private bool IsValidCEP(string cep)
    {
        // Implementar a lógica de validação do CEP
        return true; // Placeholder para a lógica de validação real
    }

    #endregion
}