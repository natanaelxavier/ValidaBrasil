using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// O Cadastro de Pessoa Física (CPF) é um número de identificação atribuído a pessoas físicas pela Receita Federal do Brasil. 
    /// Ele é utilizado para registrar os contribuintes no sistema tributário nacional e para diversas transações fiscais e legais. 
    /// Qualquer pessoa natural, independente de idade ou nacionalidade, pode ser registrada, incluindo pessoas falecidas.
    /// </summary>
    internal class Cpf : IOperacao
    {
        public string Formatar(string cpf)
        {
            cpf = RemoverFormatacao(cpf);
            return Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }
        
        public string RemoverFormatacao(string cpf)
        {
            return Regex.Replace(cpf, @"[^\d]", "");
        }

        public bool Validar(string cpf)
        {
            cpf = RemoverFormatacao(cpf);

            if (!Regex.IsMatch(cpf, @"^\d{11}$") || new string(cpf[0], 11) == cpf)
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}
