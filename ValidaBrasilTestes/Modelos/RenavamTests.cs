using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class RenavamTests
    {
        [Fact]
        public void Deve_Validar_RENAVAM_Corretamente()
        {
            //Base
            var servico = new Renavam();
            bool resultado = servico.Validar("16669061286");
            Assert.True(resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_RENAVAM_Invalido()
        {
            //Base
            var servico = new Renavam();
            string valorInvalido = "00000000000";
            bool resultado = servico.Validar(valorInvalido);
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Formatar_RENAVAM_Corretamente()
        {
            //Base
            var servico = new Renavam();
            string valor = "16669061286";
            string resultado = servico.Formatar(valor);
            Assert.Equal("1666906128-6", resultado);
        }

        [Fact]
        public void Deve_Remover_Formatacao_RENAVAM_Corretamente()
        {
            //Base
            var servico = new Renavam();
            string valor = "1666906128-6";
            string resultado = servico.RemoverFormatacao(valor);
            Assert.Equal("16669061286", resultado);
        }

    }
}
