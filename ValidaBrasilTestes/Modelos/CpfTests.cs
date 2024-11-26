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
            var servico = new Cpf();
            bool resultado = servico.Validar("825.755.988-18");
            Assert.True(resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_CPF_Invalido()
        {
            //Base
            var servico = new Cpf();
            string valorInvalido = "123";
            bool resultado = servico.Validar(valorInvalido);
            Assert.False(resultado);
        }

        [Fact]
        public void Deve_Formatar_CPF_Corretamente()
        {
            //Base
            var servico = new Cpf();
            string valor = "12345678901";
            string resultado = servico.Formatar(valor);
            Assert.Equal("123.456.789-01", resultado);
        }

        [Fact]
        public void Deve_Remover_Formatacao_CPF_Corretamente()
        {
            //Base
            var servico = new Cpf();
            string valor = "123.456.789-01";
            string resultado = servico.RemoverFormatação(valor);
            Assert.Equal("12345678901", resultado);
        }
    }
}
