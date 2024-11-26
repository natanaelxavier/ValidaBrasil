﻿using System;
using ValidaBrasil.Enums;
using ValidaBrasil.Interfaces;
using ValidaBrasil.Modelos;

namespace ValidaBrasil.Servicos
{
    public class FormatacaoServico
    {
        public string Formatar(TipoDocumento tipo, string valor)
        {
            IOperacao operacao;

            switch (tipo)
            {
                case TipoDocumento.CPF:
                    operacao = new Cpf();
                    break;
                case TipoDocumento.CNPJ:
                    operacao = new Cnpj();
                    break;
                case TipoDocumento.RENAVAM:
                    operacao = new Renavam();
                    break;
                default:
                    throw new ArgumentException($"Tipo {tipo} não suportado.");
            }

            return operacao.Formatar(valor);
        }

        public string RemoverFormatacao(TipoDocumento tipo, string valor)
        {
            IOperacao operacao;

            switch (tipo)
            {
                case TipoDocumento.CPF:
                    operacao = new Cpf();
                    break;
                case TipoDocumento.CNPJ:
                    operacao = new Cnpj();
                    break;
                case TipoDocumento.RENAVAM:
                    operacao = new Renavam();
                    break;
                default:
                    throw new ArgumentException($"Tipo {tipo} não suportado.");
            }

            return operacao.RemoverFormatação(valor);
        }
    }
}
