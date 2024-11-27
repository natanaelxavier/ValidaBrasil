using System;
using System.Collections.Generic;
using System.Text;
using ValidaBrasil.Enums;
using ValidaBrasil.Modelos;
using Xunit;

namespace ValidaBrasil.Tests.Modelos
{
    public class PlacaVeiculoTests
    {
        [Theory]
        [InlineData("ABC-1234", TipoPlacaVeiculo.ANTIGA)] // Placa antiga com hífen
        [InlineData("ABC1234", TipoPlacaVeiculo.ANTIGA)]  // Placa antiga sem hífen
        [InlineData("ABC1D23", TipoPlacaVeiculo.MERCOSUL)] // Placa Mercosul
        [InlineData("ABC-1D23", TipoPlacaVeiculo.MERCOSUL)] // Placa Mercosul com hífen
        [InlineData("1234567", null)] // Placa inválida
        internal void Deve_Identificar_TipoDePlaca_Corretamente(string placa, TipoPlacaVeiculo? esperado)
        {
            // Arrange
            var servico = new PlacaVeiculo();

            // Act
            var tipo = servico.ObterTipo(placa);

            // Assert
            Assert.Equal(esperado, tipo);
        }

        [Theory]
        [InlineData("ABC-1234", true)] // Placa antiga válida
        [InlineData("ABC1234", true)]  // Placa antiga sem hífen válida
        [InlineData("ABC1D23", true)]  // Placa Mercosul válida
        [InlineData("A1BC123", false)] // Placa inválida
        [InlineData("123-ABCD", false)] // Placa inválida
        [InlineData("AB123CD", false)] // Placa inválida
        public void Deve_Validar_PLACAVEICULO_Corretamente(string placa, bool esperado)
        {
            //Base
            var servico = new PlacaVeiculo();
            bool resultado = servico.Validar(placa);
            Assert.Equal(esperado, resultado);
        }

        [Fact]
        public void Deve_Retornar_Falso_Para_PLACAVEICULO_Invalido()
        {
            //Base
            var servico = new PlacaVeiculo();
            bool resultado1 = servico.Validar("A1BC123");
            bool resultado2 = servico.Validar("123-ABCD");
            Assert.False(resultado1);
            Assert.False(resultado2);
        }

        [Theory]
        [InlineData("ABC1234", "ABC-1234")] // Placa antiga
        [InlineData("ABC1D23", "ABC1D23")]  // Placa Mercosul
        public void Deve_Formatar_PLACAVEICULO_Corretamente(string placa, string esperado)
        {
            //Base
            var servico = new PlacaVeiculo();
            string resultado = servico.Formatar(placa);
            Assert.Equal(resultado, esperado);
        }

        [Theory]
        [InlineData("ABC-1234", "ABC1234")] // Placa antiga
        [InlineData("ABC1D23", "ABC1D23")]  // Placa Mercosul
        public void Deve_Remover_Formatacao_PLACAVEICULO_Corretamente(string placa, string esperado)
        {
            //Base
            var servico = new PlacaVeiculo();
            string resultado = servico.RemoverFormatacao(placa);
            Assert.Equal(resultado, esperado);
        }
    }
}
