using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Enums;
using ValidaBrasil.Interfaces;
using ValidaBrasil.Modelos;

namespace ValidaBrasil.Servicos
{
    public class ValidacaoServico
    {
        public bool Validar(TipoDocumento tipo, string valor)
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
                case TipoDocumento.TITULOELEITORAL:
                    operacao = new TituloEleitoral();
                    break;
                case TipoDocumento.PLACAVEICULO:
                    operacao = new PlacaVeiculo();
                    break;
                default:
                    throw new ArgumentException($"Tipo {tipo} não suportado.");
            }

            return operacao.Validar(valor);
        }
    }
}
