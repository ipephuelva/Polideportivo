using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.Models;

namespace NetCore.Unit.Tests
{
    [TestClass]
    public class PistasTests
    {
        [TestMethod]
        public void Pista_Invalido()
        {
            var pista = new Pistas() { Sport = string.Empty, NField = 4 };

            var resultado = pista.EsValido(pista);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Pista_Valido()
        {
            var pista = new Pistas() { Sport = "Futbol", NField = 4 };

            var resultado = pista.EsValido(pista);

            Assert.IsTrue(resultado);
        }

    }
}
