using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    internal class Telefone : IOperacao
    {
        // Atributos Internos
        private readonly Regex regexComPais = new Regex(@"^(\+?55\s?)?(\(?\d{2}\)?\s?)?(9?\d{4}-?\d{4}|9?\d{8})$"); // Com código do país


        public string Formatar(string telefone)
        {
            telefone = RemoverFormatacao(telefone);

            if (telefone.Length < 8 || telefone.Length > 14)
                throw new ArgumentException("Número de telefone inválido.");

            if (telefone.Length >= 13 && !telefone.StartsWith("55"))
                return telefone;

            string codigoPais = "";
            if (telefone.StartsWith("55"))
            {
                codigoPais = $"+{telefone.Substring(0, 2)} ";
                telefone = telefone.Substring(2);
            }

            string ddd = "";
            if(telefone.Length >= 10)
            {
                int qtd_index = telefone.Length.Equals(11) ? telefone[2] == '9' ? 2 : 3 : 2;
                ddd = $"({telefone.Substring(0, qtd_index)}) ";
                telefone = telefone.Substring(qtd_index);
            }

            string partePrincipal = telefone.Length > 9 ? telefone.Substring(2) : telefone;

            if (partePrincipal.Length == 9) // Com nono dígito
            {
                return $"{codigoPais}{ddd}{partePrincipal.Substring(0, 5)}-{partePrincipal.Substring(5)}";
            }
            else // Sem nono dígito
            {
                return $"{codigoPais}{ddd}{partePrincipal.Substring(0, 4)}-{partePrincipal.Substring(4)}";
            }
        }

        public string RemoverFormatacao(string telefone)
        {
            return Regex.Replace(telefone, @"[^0-9]", "");
        }

        public bool Validar(string telefone)
        {
            telefone = telefone.Trim();

            // Validações baseadas nos diferentes padrões
            if (regexComPais.IsMatch(telefone))
            {
                telefone = RemoverFormatacao(telefone);

                if (telefone.Length < 8 || telefone.Length > 13)
                    return false;

                if (telefone.Length > 9) // Com DDD ou código de país
                {
                    string ddd = telefone.Length > 11 ? telefone.Substring(2, 2) : telefone.Substring(0, 2);
                    return ValidarDDD(ddd);
                }
                return true; // Sem DDD, apenas o número principal
            }
            return false;
        }

        /// <summary>
        /// Valida se o DDD é válido.
        /// </summary>
        private bool ValidarDDD(string ddd)
        {
            string[] dddsValidos = {
                "11", "12", "13", "14", "15", "16", "17", "18", "19", "21", "22", "24",
                "27", "28", "31", "32", "33", "34", "35", "37", "38", "41", "42", "43",
                "44", "45", "46", "47", "48", "49", "51", "53", "54", "55", "61", "62",
                "63", "64", "65", "66", "67", "68", "69", "71", "73", "74", "75", "77",
                "79", "81", "82", "83", "84", "85", "86", "87", "88", "89", "91", "92",
                "93", "94", "95", "96", "97", "98", "99"
            };

            return Array.Exists(dddsValidos, d => d == ddd);
        }
    }
}
