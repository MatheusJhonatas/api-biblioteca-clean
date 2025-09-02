using System;
using System.Linq;

namespace Biblioteca.Domain.ValueObjects
{
    /// <summary>
    /// Value Object responsável por encapsular a lógica e validação de um CPF.
    /// </summary>
    public record class CPF : ValueObject
    {
        #region Propriedades
        public string Numero { get; }

        #endregion

        #region Construtores
        public CPF() { }

        public CPF(string numero)
        {
            if (numero == null)
                throw new ArgumentException("CPF não pode ser nulo.", nameof(numero));
            if (string.IsNullOrWhiteSpace(numero))
                throw new ArgumentException("CPF não pode ser vazio.", nameof(numero));

            var cpf = numero.Replace(".", "").Replace("-", "").Trim();

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                throw new ArgumentException("CPF deve conter 11 dígitos numéricos.", nameof(numero));

            if (!IsValid(cpf))
                throw new ArgumentException("CPF inválido.", nameof(numero));

            Numero = cpf;
        }

        #endregion

        #region Métodos de Validação

        /// <summary>
        /// Verifica se o CPF é válido com base na regra dos dígitos verificadores.
        /// </summary>
        /// <param name="cpf">CPF apenas com dígitos numéricos.</param>
        /// <returns>True se o CPF for válido, false caso contrário.</returns>
        private bool IsValid(string cpf)
        {
            // remove sequências inválidas (tipo 11111111111)
            if (new string(cpf[0], 11) == cpf) return false;
            // Verifica se tem 11 dígitos e não são todos iguais
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        #endregion

        #region Métodos Utilitários

        /// <summary>
        /// Retorna o CPF formatado com máscara: 000.000.000-00
        /// </summary>
        /// <returns>CPF formatado.</returns>
        public override string ToString()
        {
            return Convert.ToUInt64(Numero).ToString(@"000\.000\.000\-00");
        }

        /// <summary>
        /// Compara dois objetos CPF por igualdade de valor.
        /// </summary>
        /// <param name="obj">Outro objeto CPF.</param>
        /// <returns>True se os números forem iguais.</returns>
        // public override bool Equals(object obj)
        // {
        //     if (obj is null || GetType() != obj.GetType())
        //         return false;

        //     var other = (CPF)obj;
        //     return Numero == other.Numero;
        // }

        /// <summary>
        /// Retorna o código hash do CPF.
        /// </summary>
        /// <returns>Código hash baseado no número do CPF.</returns>
        public override int GetHashCode()
        {
            return Numero.GetHashCode();
        }

        #endregion
    }
}
