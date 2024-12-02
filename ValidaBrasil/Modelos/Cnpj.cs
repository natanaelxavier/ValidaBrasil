using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// O Cadastro Nacional da Pessoa Jurídica (CNPJ) é um número único atribuído a 
    /// entidades jurídicas, como empresas, órgãos públicos, e outros tipos de arranjos jurídicos 
    /// que possuem ou não personalidade jurídica. O CNPJ é emitido pela Receita Federal do Brasil, 
    /// sendo utilizado para identificar essas entidades perante o governo e em transações legais e fiscais.
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
