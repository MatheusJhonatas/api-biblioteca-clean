// Atributos: Rua, Número, Bairro, Cidade, Estado, CEP
// Regras: Todos os campos obrigatórios; CEP deve ser válido
// Uso: User, possivelmente Library
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Biblioteca.Domain.ValueObjects;

public record class Endereco : ValueObject
{
    #region Propriedades
    public string Rua { get; private set; }
    public string Complemento { get; set; }
    public string Numero { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string CEP { get; private set; }
    #endregion

    #region Construtores
    public Endereco() { }
    public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
    {
        if (string.IsNullOrWhiteSpace(rua)) throw new ArgumentException("Rua é obrigatória.");
        if (string.IsNullOrWhiteSpace(numero)) throw new ArgumentException("Número é obrigatório.");
        if (string.IsNullOrWhiteSpace(bairro)) throw new ArgumentException("Bairro é obrigatório.");
        if (string.IsNullOrWhiteSpace(cidade)) throw new ArgumentException("Cidade é obrigatória.");
        if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Estado é obrigatório.");
        if (!IsValidCEP(cep)) throw new ArgumentException("CEP inválido. Deve conter exatamente 8 dígitos numéricos.");

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
        return Regex.IsMatch(cep, @"^\d{8}$");
    }

    #endregion
}