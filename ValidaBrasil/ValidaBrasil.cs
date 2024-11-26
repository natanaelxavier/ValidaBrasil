using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Enums;
using ValidaBrasil.Servicos;

namespace ValidaBrasil
{
    public static class ValidaBrasil
    {
        private static readonly ValidacaoServico _validacaoServico = new ValidacaoServico();
        private static readonly FormatacaoServico _formatacaoServico = new FormatacaoServico();
    
        public static bool Validacao(TipoDocumento tipo, string valor)
        {
            return _validacaoServico.Validar(tipo, valor);
        }

        public static string Formatacao(TipoDocumento tipo, string valor)
        {
            return _formatacaoServico.Formatar(tipo, valor);
        }

        public static string RemoverFormatacao(TipoDocumento tipo, string valor)
        {
            return _formatacaoServico.RemoverFormatacao(tipo, valor);
        }
    }
}
