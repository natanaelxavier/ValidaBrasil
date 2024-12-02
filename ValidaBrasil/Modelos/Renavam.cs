using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// O Registro Nacional de Veículos Automotores (RENAVAM) é um sistema utilizado no Brasil para registrar todos os veículos do país.
    /// A principal finalidade do RENAVAM é centralizar o registro dos veículos, realizado pelas unidades do Detran em cada estado, 
    /// e organizado pela unidade central, o Denatran. Este sistema é fundamental para a gestão e controle do transporte de veículos no Brasil.
    /// </summary>
    internal class Renavam : IOperacao
    {
        public string Formatar(string renavam)
        {
            renavam = RemoverFormatacao(renavam);
            return Regex.Replace(renavam, @"(\d{10})(\d)", "$1-$2");
        }

        public string RemoverFormatacao(string renavam)
        {
            return Regex.Replace(renavam, @"[^\d]", "");
        }

        public bool Validar(string renavam)
        {
            renavam = RemoverFormatacao(renavam);
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
