using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class TelefoneTests
    {
        [Theory]
        [InlineData("558412345678", "+55 (84) 1234-5678")]
        [InlineData("5508412345678", "+55 (084) 1234-5678")]
        [InlineData("8412345678", "(84) 1234-5678")]
        [InlineData("08412345678", "(084) 1234-5678")]
        [InlineData("12345678", "1234-5678")]
        [InlineData("912345678", "91234-5678")]
        [InlineData("5584912345678", "+55 (84) 91234-5678")]
        internal void Deve_Formatar_NumeroTelefone_Corretamente(string telefone, string esperado)
        {
            //Base
            var servico = new Telefone();
            string resultado = servico.Formatar(telefone);
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData("+55 (84) 1234-5678", "558412345678")]
        [InlineData("+55 (084) 1234-5678", "5508412345678")]
        [InlineData("(84) 1234-5678", "8412345678")]
        [InlineData("(084) 1234-5678", "08412345678")]
        [InlineData("1234-5678", "12345678")]
        [InlineData("91234-5678", "912345678")]
        [InlineData("+55 (84) 91234-5678", "5584912345678")]
        internal void Deve_Remover_Formatacao_NumeroTelefone_Corretamente(string telefone, string esperado)
        {
            //Base
            var servico = new Telefone();
            string resultado = servico.RemoverFormatacao(telefone);
            Assert.Equal(esperado, resultado);
        }

        [Theory]
        [InlineData("+55 (84) 1234-5678", true)]
        [InlineData("5508412345678", false)]
        [InlineData("8412345678", true)]
        [InlineData("08412345678", false)]
        [InlineData("1234-5678", true)]
        [InlineData("91234-5678", true)]
        [InlineData("+55 (84) 91234-5678", true)]
        [InlineData("+55 (1234) 91234-5678", false)]
        [InlineData("+55 (05) 91234-5678", false)]
        [InlineData("0591234-5678", false)]
        [InlineData("0591234", false)]
        internal void Deve_Validar_NumeroTelefone_Corretamente(string telefone, bool esperado)
        {
            //Base
            var servico = new Telefone();
            bool resultado = servico.Validar(telefone);
            Assert.Equal(esperado, resultado);
        }
    }
}
