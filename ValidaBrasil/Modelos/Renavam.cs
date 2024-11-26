using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// O Registro Nacional de Veículos Automotores (RENAVAM) 
    /// é um sistema desenvolvido pelo Serpro que cobre todo o Brasil,
    /// tendo como principal finalidade o registro de todos os veículos do país, 
    /// efetuados pelas unidades do Detran em cada estado, e centralizados pela unidade central, o Denatran.
    /// Autor: Wikipedia
    /// </summary>
    internal class Renavam : IOperacao
    {
        public string Formatar(string renavam)
        {
            renavam = RemoverFormatação(renavam);
            return Regex.Replace(renavam, @"(\d{10})(\d)", "$1-$2");
        }

        public string RemoverFormatação(string renavam)
        {
            return Regex.Replace(renavam, @"[^\d]", "");
        }

        public bool Validar(string renavam)
        {
            renavam = RemoverFormatação(renavam);
            renavam = renavam.PadRight(11, '0');

            if (string.IsNullOrEmpty(renavam) || renavam.Length != 11 || !Regex.IsMatch(renavam, "^[0-9]{11}$"))
                return false;

            if (new string(renavam[0], renavam.Length) == renavam)
                return false;

            string baseRenavam = renavam.Substring(0, 10);
            int[] digitosInvertidos = ObterDigitosInvertidos(baseRenavam);
            int soma = ObterSoma(digitosInvertidos);
            int verificadorCalculado = ObterVerificador(soma);
            int verificador = ObterDigitoVerificador(renavam);

            return verificadorCalculado == verificador;
        }

        private static int[] ObterDigitosInvertidos(string renavam)
        {
            char[] digitosChar = renavam.ToCharArray();
            Array.Reverse(digitosChar);
            return Array.ConvertAll(digitosChar, c => (int)Char.GetNumericValue(c));
        }
        private static int ObterDigitoVerificador(string renavam)
        {
            return int.Parse(renavam[10].ToString());
        }
        private static int ObterSoma(int[] digitos)
        {
            int soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += digitos[i] * ObterFactor(i);
            }
            return soma;
        }
        private static int ObterFactor(int num)
        {
            int[] fatores = { 2, 3, 4, 5, 6, 7, 8, 9 };
            return fatores[num % fatores.Length];
        }
        private static int ObterVerificador(int soma)
        {
            int valor = 11 - (soma % 11);
            return valor >= 10 ? 0 : valor;
        }
    }
}
