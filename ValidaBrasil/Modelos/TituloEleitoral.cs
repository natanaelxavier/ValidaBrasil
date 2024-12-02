using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Enums;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// O Título de Eleitor é o documento que certifica que um cidadão está registrado na Justiça Eleitoral 
    /// e tem o direito de participar ativamente do processo eleitoral, seja votando ou sendo votado em eleições 
    /// em níveis municipal, estadual ou federal no Brasil.
    /// </summary>
    internal class TituloEleitoral : IOperacao
    {
        public string Formatar(string tituloeleitoral)
        {
            tituloeleitoral = RemoverFormatacao(tituloeleitoral);
            return Regex.Replace(tituloeleitoral, @"(\d{4})(\d{4})(\d{4})", "$1 $2 $3");
        }

        public string RemoverFormatacao(string tituloeleitoral)
        {
            return Regex.Replace(tituloeleitoral, @"[^\d]", "");
        }

        public bool Validar(string tituloeleitoral)
        {
            tituloeleitoral = RemoverFormatacao(tituloeleitoral);

            if (string.IsNullOrEmpty(tituloeleitoral) || tituloeleitoral.Length != 12 || !Regex.IsMatch(tituloeleitoral, "^[0-9]{12}$"))
                return false;

            if (new string(tituloeleitoral[0], tituloeleitoral.Length) == tituloeleitoral)
                return false;

            string numero = tituloeleitoral.Substring(0, 8);
            string estado = tituloeleitoral.Substring(8, 2);
            string dv = tituloeleitoral.Substring(10, 2);

            int[] numeros = Array.ConvertAll(numero.ToCharArray(), c => (int)char.GetNumericValue(c));
            int estadoInt = int.Parse(estado);
            int dv1 = int.Parse(dv[0].ToString());
            int dv2 = int.Parse(dv[1].ToString());
            
            int verificador1 = CalcularPrimeiroDigito(numeros, 9);
            int verificador2 = CalcularSegundoDigito(verificador1, estadoInt);

            return dv1 == verificador1 && dv2 == verificador2;
        }

        private int CalcularPrimeiroDigito(int[] numeros, int pesoInicial)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
            {
                soma += numeros[i] * (pesoInicial - (numeros.Length - (1 + i)));
            }
            int digito = soma % 11;
            return digito == 10 ? 0 : digito;
        }

        private int CalcularSegundoDigito(int verificador1, int estadoInt)
        {
            int soma = (estadoInt / 10) * 7 + (estadoInt % 10) * 8 + verificador1 * 9;
            int digito = soma % 11;
            return digito == 10 ? 0 : digito;
        }
    }
}
