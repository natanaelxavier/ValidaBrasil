using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// No Brasil, o Cadastro Nacional da Pessoa Jurídica (acrônimo: CNPJ)
    /// é um número único que identifica uma pessoa jurídica e outros tipos de arranjo jurídico 
    /// sem personalidade jurídica (como condomínios, órgãos públicos, fundos) junto à 
    /// Receita Federal brasileira (órgão do Ministério da Economia).
    /// Autor: Wikipedia
    /// </summary>
    internal class Cnpj : IOperacao
    {
        public string Formatar(string cnpj)
        {
            cnpj = RemoverFormatacao(cnpj);
            return Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }

        public string RemoverFormatacao(string cnpj)
        {
            return Regex.Replace(cnpj, @"[^\d]", "");
        }

        public bool Validar(string cnpj)
        {
            cnpj = RemoverFormatacao(cnpj);

            if (!Regex.IsMatch(cnpj, @"^\d{14}$") || new string(cnpj[0], 14) == cnpj)
                return false;

            int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}
