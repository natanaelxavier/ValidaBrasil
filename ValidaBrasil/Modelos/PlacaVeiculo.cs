using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using ValidaBrasil.Enums;
using ValidaBrasil.Interfaces;

namespace ValidaBrasil.Modelos
{
    /// <summary>
    /// As placas de identificação de veículos no Brasil são emitidas pelos Departamentos Estaduais de Trânsito (DETRANs) 
    /// de cada estado e do Distrito Federal, e seguem um sistema alfanumérico único, que é padronizado em todo o território nacional.
    /// Essas placas servem para identificar e registrar veículos em circulação no país.
    /// </summary>
    internal class PlacaVeiculo : IOperacao
    {
        // Atributos Internos
        private readonly Regex regexAntigo = new Regex(@"^[A-Z]{3}-?\d{4}$");
        private readonly Regex regexMercosul = new Regex(@"^[A-Z]{3}-?\d[A-Z]\d{2}$");

        // Metodos
        public string Formatar(string placa)
        {
            placa = RemoverFormatacao(placa);

            if (placa.Length != 7)
                throw new ArgumentException("Placa inválida: comprimento incorreto.");

            TipoPlacaVeiculo? tipo = ObterTipo(placa);

            if (tipo == TipoPlacaVeiculo.ANTIGA)
            {
                return $"{placa.Substring(0, 3)}-{placa.Substring(3, 4)}";
            }
            else if (tipo == TipoPlacaVeiculo.MERCOSUL)
            {
                return $"{placa.Substring(0, 3)}{placa[3]}{placa[4]}{placa.Substring(5)}";
            }
            else
            {
                return placa;
            }
        }

        public string RemoverFormatacao(string placa)
        {
            return Regex.Replace(placa, @"[^A-Za-z0-9]", "");
        }

        public bool Validar(string placa)
        {
            placa = placa.Trim().ToUpper();

            string placaSemFormatacao = RemoverFormatacao(placa);

            if (placaSemFormatacao.Length != 7)
                return false;

            TipoPlacaVeiculo? tipo = ObterTipo(placa);

            if (tipo == null)
                return false;

            if (tipo == TipoPlacaVeiculo.ANTIGA)
            {
                return ValidarPlacaAntiga(placaSemFormatacao);
            }
            else if (tipo == TipoPlacaVeiculo.MERCOSUL)
            {
                return ValidarPlacaMercosul(placaSemFormatacao);
            }

            return false;
        }

        internal TipoPlacaVeiculo? ObterTipo(string placa)
        {
            if (this.regexAntigo.IsMatch(placa))
            {
                return TipoPlacaVeiculo.ANTIGA;
            }
            else if (this.regexMercosul.IsMatch(placa))
            {
                return TipoPlacaVeiculo.MERCOSUL;
            }
            else
            {
                return null;
            }
        }

        private bool ValidarPlacaAntiga(string placa)
        {
            if (!regexAntigo.IsMatch(placa))
                return false;

            string letras = placa.Substring(0, 3);
            return !letras.Contains("Q") && !letras.Contains("W") && !letras.Contains("Y");
        }

        private bool ValidarPlacaMercosul(string placa)
        {
            return regexMercosul.IsMatch(placa);
        }
    }
}
