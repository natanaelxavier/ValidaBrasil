using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class TituloEleitoralTests
    {
        [Fact]
        public void Deve_Validar_TITULOELEITORAL_Corretamente()
        {
            //Base
            var servico = new TituloEleitoral();
            bool resultado = servico.Validar("071383510140");
            Assert.True(resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_TITULOELEITORAL_Invalido()
        {
            //Base
            var servico = new TituloEleitoral();
            string valorInvalido = "000000000000";
            bool resultado = servico.Validar(valorInvalido);
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Formatar_TITULOELEITORAL_Corretamente()
        {
            //Base
            var servico = new TituloEleitoral();
            string valor = "324101221686";
            string resultado = servico.Formatar(valor);
            Assert.Equal("3241 0122 1686", resultado);
        }

        [Fact]
        public void Deve_Remover_Formatacao_TITULOELEITORAL_Corretamente()
        {
            //Base
            var servico = new TituloEleitoral();
            string valor = "3241 0122 1686";
            string resultado = servico.RemoverFormatacao(valor);
            Assert.Equal("324101221686", resultado);
        }
    }
}
