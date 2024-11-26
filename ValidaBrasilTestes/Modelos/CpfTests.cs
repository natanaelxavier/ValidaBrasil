using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class CpfTests
    {
        [Fact]
        public void Deve_Validar_CPF_Corretamente()
        {
            //Base
            var cpf = new Cpf();
            bool resultado = cpf.Validar("825.755.988-18");
            Assert.True(resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_CPF_Invalido()
        {
            //Base
            var cpf = new Cpf();
            string valorInvalido = "123";
            bool resultado = cpf.Validar(valorInvalido);
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Formatar_CPF_Corretamente()
        {
            //Base
            var cpf = new Cpf();
            string valor = "12345678901";
            string resultado = cpf.Formatar(valor);
            Assert.Equal("123.456.789-01", resultado);
        }

        [Fact]
        public void Deve_Remover_Formatacao_CPF_Corretamente()
        {
            //Base
            var cpf = new Cpf();
            string valor = "123.456.789-01";
            string resultado = cpf.RemoverFormatação(valor);
            Assert.Equal("12345678901", resultado);
        }
    }
}
