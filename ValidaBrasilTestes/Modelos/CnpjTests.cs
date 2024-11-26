using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class CnpjTests
    {
        [Fact]
        public void Deve_Validar_CNPJ_Corretamente()
        {
            //Base
            var servico = new Cnpj();
            bool resultado = servico.Validar("52.025.034/0001-34");
            Assert.True(resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_CNPJ_Invalido()
        {
            //Base
            var servico = new Cnpj();
            string valorInvalido = "123";
            bool resultado = servico.Validar(valorInvalido);
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Formatar_CNPJ_Corretamente()
        {
            //Base
            var servico = new Cnpj();
            string valor = "12345678012345";
            string resultado = servico.Formatar(valor);
            Assert.Equal("12.345.678/0123-45", resultado);
        }

        [Fact]
        public void Deve_Remover_Formatacao_CNPJ_Corretamente()
        {
            //Base
            var servico = new Cnpj();
            string valor = "12.345.678/0123-45";
            string resultado = servico.RemoverFormatação(valor);
            Assert.Equal("12345678012345", resultado);
        }
    }
}
